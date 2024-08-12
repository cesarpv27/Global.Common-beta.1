
namespace Global.Common.Extensions
{
    /// <summary>
    /// Provides extension methods for working with Tasks.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Waits for the completion of the specified <paramref name="task"/> and unwraps any exceptions that occurred during its execution, using <see cref="ExceptionExtensions.PrepareForReThrow"/>.
        /// </summary>
        /// <param name="task">The task to wait for and unwrap exceptions from.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="task"/> is null.</exception>
        /// <exception cref="AggregateException">Thrown if the <paramref name="task"/> produces an unhandled exception.</exception>
        /// <exception cref="TaskCanceledException">Thrown if the task was canceled.</exception>
        /// <exception cref="Exception">Thrown if the task completed in a Faulted state.</exception>
        public static void WaitAndUnwrapException(this Task task)
        {
            AssertHelper.AssertNotNullOrThrow(task, nameof(task));

            try
            {
                task.GetAwaiter().GetResult();
            }
            catch (AggregateException ex)
            {
                throw ex.PrepareForReThrow();
            }
        }

        /// <summary>
        /// Waits for the completion of the specified <paramref name="task"/> and unwraps any exceptions that occurred during its execution, using <see cref="ExceptionExtensions.PrepareForReThrow"/>.
        /// </summary>
        /// <typeparam name="TOut">The type of result returned by the task.</typeparam>
        /// <param name="task">The task to wait for and unwrap exceptions from.</param>
        /// <returns>The result produced by the task.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="task"/> is null.</exception>
        /// <exception cref="AggregateException">Thrown if the <paramref name="task"/> produces an unhandled exception.</exception>
        /// <exception cref="TaskCanceledException">Thrown if the task was canceled.</exception>
        /// <exception cref="Exception">Thrown if the task completed in a Faulted state.</exception>
        public static TOut WaitAndUnwrapException<TOut>(this Task<TOut> task)
        {
            AssertHelper.AssertNotNullOrThrow(task, nameof(task));

            try
            {
                return task.GetAwaiter().GetResult();
            }
            catch (AggregateException ex)
            {
                throw ex.PrepareForReThrow();
            }
        }

        /// <summary>
        /// Waits for the completion of the specified <paramref name="task"/> and unwraps any exceptions that occurred during its execution, using <see cref="ExceptionExtensions.PrepareForReThrow"/>.
        /// </summary>
        /// <param name="task">The task to wait for and unwrap exceptions from.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel waiting for the task.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="task"/> is null.</exception>
        /// <exception cref="AggregateException">Thrown if the <paramref name="task"/> produces an unhandled exception.</exception>
        /// <exception cref="OperationCanceledException">Thrown if the <paramref name="cancellationToken"/> was cancelled before the <paramref name="task"/> completed, or the <paramref name="task"/> raised an <see cref="OperationCanceledException"/>.</exception>
        /// <exception cref="ObjectDisposedException">Thrown if an object was disposed before the execution.</exception>
        public static void WaitAndUnwrapException(this Task task, CancellationToken cancellationToken)
        {
            AssertHelper.AssertNotNullOrThrow(task, nameof(task));

            try
            {
                task.Wait(cancellationToken);
            }
            catch (AggregateException ex)
            {
                throw ex.PrepareForReThrow();
            }
        }

        /// <summary>
        /// Waits for the completion of the specified <paramref name="task"/> and unwraps any exceptions that occurred during its execution, using <see cref="ExceptionExtensions.PrepareForReThrow"/>.
        /// </summary>
        /// <typeparam name="TResult">The type of result returned by the task.</typeparam>
        /// <param name="task">The task to wait for and unwrap exceptions from.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel waiting for the task.</param>
        /// <returns>The result produced by the task.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="task"/> is null.</exception>
        /// <exception cref="AggregateException">Thrown if the <paramref name="task"/> produces an unhandled exception.</exception>
        /// <exception cref="OperationCanceledException">Thrown if the <paramref name="cancellationToken"/> was cancelled before the <paramref name="task"/> completed, or the <paramref name="task"/> raised an <see cref="OperationCanceledException"/>.</exception>
        /// <exception cref="ObjectDisposedException">Thrown if an object was disposed before the execution.</exception>
        public static TResult WaitAndUnwrapException<TResult>(this Task<TResult> task, CancellationToken cancellationToken)
        {
            AssertHelper.AssertNotNullOrThrow(task, nameof(task));

            try
            {
                task.Wait(cancellationToken);
                return task.Result;
            }
            catch (AggregateException ex)
            {
                throw ex.PrepareForReThrow();
            }
        }

        /// <summary>
        /// WARNING: This method catches any exception thrown during the execution of <paramref name="task"/>.
        /// Waits for the completion of the specified <paramref name="task"/> and catches any exceptions that may occur during its execution.
        /// </summary>
        /// <param name="task">The task to wait for and catch exceptions from.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="task"/> is null.</exception>
        public static void WaitCatchAnyException(this Task task)
        {
            AssertHelper.AssertNotNullOrThrow(task, nameof(task));

            try
            {
                task.Wait();
            }
            catch
            {
            }
        }

        /// <summary>
        /// WARNING: This method catches any exception thrown during the execution of <paramref name="task"/>.
        /// Waits for the completion of the specified <paramref name="task"/> and catches any exceptions that may occur during its execution, except if the execution was canceled before the <paramref name="task"/> completed.
        /// </summary>
        /// <param name="task"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="task"/> is null.</exception>
        /// <exception cref="OperationCanceledException">Thrown if the <paramref name="cancellationToken"/> was cancelled before the <paramref name="task"/> completed, or the <paramref name="task"/> raised an <see cref="OperationCanceledException"/>.</exception>
        public static void WaitWithoutException(this Task task, CancellationToken cancellationToken)
        {
            AssertHelper.AssertNotNullOrThrow(task, nameof(task));

            try
            {
                task.Wait(cancellationToken);
            }
            catch
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
        }
    }
}
