namespace AirBnBWebApi.Api.Helpers;

public class AsyncHandler
{
    public static Func<T, Task> HandleAsync<T>(Func<T, Task> func)
    {
        return async (arg) =>
        {
            try
            {
                await func(arg).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        };
    }
}
