﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NFTGenerator
{
    public static class  Extensions
    {
        public static void MoveUp(this TreeNode node)
        {
            TreeNode parent = node.Parent;
            TreeView view = node.TreeView;
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index > 0)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index - 1, node);
                }
            }
            else if (node.TreeView.Nodes.Contains(node)) //root node
            {
                int index = view.Nodes.IndexOf(node);
                if (index > 0)
                {
                    view.Nodes.RemoveAt(index);
                    view.Nodes.Insert(index - 1, node);
                }
            }
        }

        public static void MoveDown(this TreeNode node)
        {
            TreeNode parent = node.Parent;
            TreeView view = node.TreeView;
            if (parent != null)
            {
                int index = parent.Nodes.IndexOf(node);
                if (index < parent.Nodes.Count - 1)
                {
                    parent.Nodes.RemoveAt(index);
                    parent.Nodes.Insert(index + 1, node);
                }
            }
            else if (view != null && view.Nodes.Contains(node)) //root node
            {
                int index = view.Nodes.IndexOf(node);
                if (index < view.Nodes.Count - 1)
                {
                    view.Nodes.RemoveAt(index);
                    view.Nodes.Insert(index + 1, node);
                }
            }
        }

        /// <summary>
        /// Concurrently Executes async actions for each item of <see cref="IEnumerable<typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Type of IEnumerable</typeparam>
        /// <param name="enumerable">instance of <see cref="IEnumerable<typeparamref name="T"/>"/></param>
        /// <param name="action">an async <see cref="Action" /> to execute</param>
        /// <param name="maxDegreeOfParallelism">Optional, An integer that represents the maximum degree of parallelism,
        /// Must be grater than 0</param>
        /// <returns>A Task representing an async operation</returns>
        /// <exception cref="ArgumentOutOfRangeException">If the maxActionsToRunInParallel is less than 1</exception>
        public static async Task ForEachAsyncConcurrent<T>(
            this IEnumerable<T> enumerable,
            Func<T, Task> action,
            int? maxDegreeOfParallelism = null)
        {
            if (maxDegreeOfParallelism.HasValue)
            {
                using (var semaphoreSlim = new SemaphoreSlim(
                    maxDegreeOfParallelism.Value, maxDegreeOfParallelism.Value))
                {
                    var tasksWithThrottler = new List<Task>();

                    foreach (var item in enumerable)
                    {
                        // Increment the number of currently running tasks and wait if they are more than limit.
                        await semaphoreSlim.WaitAsync();

                        tasksWithThrottler.Add(Task.Run(async () =>
                        {
                            await action(item).ContinueWith(res =>
                            {
                                // action is completed, so decrement the number of currently running tasks
                                semaphoreSlim.Release();
                            });
                        }));
                    }

                    // Wait for all tasks to complete.
                    await Task.WhenAll(tasksWithThrottler.ToArray());
                }
            }
            else
            {
                await Task.WhenAll(enumerable.Select(item => action(item)));
            }
        }

        public static byte[] Hash(this string inputString)
        {
            return MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string HashString(this string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in  inputString.Hash())
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
