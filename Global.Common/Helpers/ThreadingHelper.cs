
namespace Global.Common.Helpers
{
    /// <summary>
    /// Provides helper methods for working with Parallel and Threading.
    /// </summary>
    public static class ThreadingHelper
    {
        #region Async

        /// <summary>
        /// Asynchronously executes the specified <paramref name="action"/>.
        /// </summary>
        /// <param name="action">The action to execute.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="action"/> is null.</exception>
        public static async Task RunAsync(Action action)
        {
            AssertHelper.AssertNotNullOrThrow(action, nameof(action));

            await Task.Run(action);
        }

        #endregion

        #region Sync

        /// <summary>
        /// Executes the specified asynchronous <paramref name="func"/> synchronously and returns the result, using the 'WaitAndUnwrapException' in <see cref="TaskExtensions"/>.
        /// </summary>
        /// <typeparam name="TIn">The type of input parameter.</typeparam>
        /// <typeparam name="TOut">The type of output parameter.</typeparam>
        /// <param name="tIn">The input parameter.</param>
        /// <param name="func">The asynchronous <see cref="Func{T, TResult}"/> to execute.</param>
        /// <returns>The result produced by the asynchronous <paramref name="func"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="func"/> is null.</exception>
        public static TOut RunSync<TIn, TOut>(TIn tIn, Func<TIn, Task<TOut>> func)
        {
            AssertHelper.AssertNotNullOrThrow(func, nameof(func));

            Task<TOut> task = Task.Run(async () => await func(tIn));

            return task.WaitAndUnwrapException();
        }

        /// <summary>
        /// Executes the specified asynchronous <paramref name="func"/> synchronously and returns the result, using the 'WaitAndUnwrapException' in <see cref="TaskExtensions"/>.
        /// </summary>
        /// <typeparam name="TIn1">The type of the first input parameter.</typeparam>
        /// <typeparam name="TIn2">The type of the second input parameter.</typeparam>
        /// <typeparam name="TOut">The type of the output parameter.</typeparam>
        /// <param name="tIn1">The first input parameter.</param>
        /// <param name="tIn2">The second input parameter.</param>
        /// <param name="func">The asynchronous <see cref="Func{T1, T2, TResult}"/> to execute.</param>
        /// <returns>The result produced by the asynchronous <paramref name="func"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="func"/> is null.</exception>
        public static TOut RunAsSync<TIn1, TIn2, TOut>(TIn1 tIn1, TIn2 tIn2, Func<TIn1, TIn2, Task<TOut>> func)
        {
            AssertHelper.AssertNotNullOrThrow(func, nameof(func));

            Task<TOut> task = Task.Run(async () => await func(tIn1, tIn2));
            return task.WaitAndUnwrapException();
        }

        #endregion

        #region Parallel ForEach

        /// <summary>
        /// Executes the specified <paramref name="func"/> in parallel for each element in the <paramref name="elemsToParallel"/> collection,
        /// validates the response using the <paramref name="funcResponseValidator"/>, and returns the number of successful executions.
        /// </summary>
        /// <typeparam name="FTIn1">The type of elements in the <paramref name="elemsToParallel"/> collection.</typeparam>
        /// <typeparam name="FTOut">The type of the output produced by the <paramref name="func"/>.</typeparam>
        /// <param name="elemsToParallel">The collection of elements to process in parallel.</param>
        /// <param name="func">The function to execute for each element.</param>
        /// <param name="funcResponseValidator">The function to validate the response produced by <paramref name="func"/>.</param>
        /// <param name="parallelOptions">Options that configure the parallel operation (optional).</param>
        /// <returns>The number of successful executions.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="elemsToParallel"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="func"/> is null.</exception>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="funcResponseValidator"/> is null.</exception>
        public static int ParallelForEachWithValidation<FTIn1, FTOut>(
            IEnumerable<FTIn1> elemsToParallel,
            Func<FTIn1, FTOut> func,
            Func<FTOut, bool> funcResponseValidator,
            ParallelOptions? parallelOptions = default)
        {
            AssertHelper.AssertNotNullOrThrow(func, nameof(elemsToParallel));
            AssertHelper.AssertNotNullOrThrow(func, nameof(func));
            AssertHelper.AssertNotNullOrThrow(func, nameof(funcResponseValidator));

            parallelOptions ??= new ParallelOptions();

            int successfulExecutions = 0;

            Parallel.ForEach(elemsToParallel, parallelOptions, () => 0, (ent, loop, subtotal) =>
            {
                if (funcResponseValidator(func(ent)))
                    subtotal++;

                return subtotal;
            },
            (finalResult) => Interlocked.Add(ref successfulExecutions, finalResult)
            );

            return successfulExecutions;
        }

        #endregion

    }
}
