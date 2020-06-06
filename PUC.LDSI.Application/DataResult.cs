using PUC.LDSI.Domain.Exception;
using System;

namespace PUC.LDSI.Application
{
    public class DataResult<TData>
    {
        public TData Data { get; set; }
        public bool Success { get; set; }
        public DomainException Exception { get; set; }

        public DataResult() 
        {
            Success = true;
        }

        public DataResult(TData data)
        {
            Success = true;

            Data = data;
        }

        public DataResult(Exception exception)
        {
            Success = false;

            if (exception is DomainException)
                Exception = exception as DomainException;
            else
                Exception = new DomainException("Erro de sistema!", exception.Message);
        }
    }
}
