using System;
using System.Threading.Tasks;

namespace KunalPattni.Services.Retry;

public static class Retry
{
    public static async Task<T> OnceAfterOneSecond<T>(Func<Task<T>> work)
    {
        return await Once(TimeSpan.FromSeconds(1), work);
    }
    
    public static async Task<T> Once<T>(TimeSpan delay, Func<Task<T>> work)
    {
        try
        {
            return await work();
        }
        catch( Exception ex)
        {
            await Task.Delay(delay);
            return await work();
        }
    }
}