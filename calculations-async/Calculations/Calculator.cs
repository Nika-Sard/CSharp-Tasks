﻿namespace Calculations;

/// <summary>
/// Presents methods for the calculation of the sum.
/// </summary>
public static class Calculator
{
    /// <summary>
    /// Calculates the sum from 1 to n synchronously.
    /// </summary>
    /// <param name="n">The last number in the sum.</param>
    /// <returns>A sum: 1 + 2 + ... + n.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Throw if n less or equals zero.</exception>
    public static long CalculateSum(int n)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n, nameof(n));
        return (long)(1 + n) * n / 2;
    }

    /// <summary>
    /// Calculates the sum from 1 to n asynchronously.
    /// </summary>
    /// <param name="n">The last number in the sum.</param>
    /// <param name="token">The cancellation token for the cancellation of the asynchronous operation.</param>
    /// <param name="progress">Presents current status of the asynchronous operation in form of the current value of sum and index.</param>
    /// <returns>A task that represents the asynchronous sum: 1 + 2 + ... + n.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Throw if n less or equals zero.</exception>
    public static async Task<long> CalculateSumAsync(int n, CancellationToken token, IProgress<(int, long)>? progress = null)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n, nameof(n));
        long sum = 0;

        await Task.Run(
            () =>
        {
            for (int i = 1; i <= n; i++)
            {
                if (token.IsCancellationRequested)
                {
                    throw new OperationCanceledException("Operation has been canceled.");
                }

                sum += i;
                progress?.Report((i, sum));
            }
        });

        return sum;
    }
}
