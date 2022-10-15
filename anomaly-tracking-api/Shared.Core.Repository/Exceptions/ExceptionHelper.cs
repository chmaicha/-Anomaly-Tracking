using System;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Shared.Core.Repository.Exceptions
{
    public class ExceptionHelper
    {
        public static ConcurrencyException HandleException(Exception exception)
        {
            ConcurrencyException output = new ConcurrencyException(exception);

            if (exception is DbUpdateConcurrencyException concurrencyEx)
            {
                // A custom exception of yours for concurrency issues
                output = new ConcurrencyException(concurrencyEx.Message);
            }
            else if (exception is DbUpdateException dbUpdateEx)
            {
                if (dbUpdateEx.InnerException != null
                        && dbUpdateEx.InnerException.InnerException != null)
                {
                    if (dbUpdateEx.InnerException.InnerException is SqlException sqlException)
                    {
                        switch (sqlException.Number)
                        {
                            case 2627:  // Unique constraint error
                                output = new ConcurrencyException("app.error.uniqueconstrainte", sqlException);
                                break;

                            case 2601:  // Duplicated key row error
                                output = new ConcurrencyException("app.error.duplicatedkeyconstrainte", sqlException);
                                break;
                        
                            case 547:   // Constraint check violation
                                output = new ConcurrencyException("app.error.checkviolationconstrainte", sqlException);
                                break;
                         
                            default:
                                // A custom exception of yours for other DB issues
                                output = new ConcurrencyException(dbUpdateEx.Message, sqlException);
                                break;
                        }
                    }
                    else
                    {
                        output = new ConcurrencyException(dbUpdateEx);
                    }
                }
            }

            return output;
        }
    }
}
