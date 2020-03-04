namespace FaustVX.Process
{
    public readonly struct ExitCode
    {
        public int Code { get; }

        public ExitCode(int code)
            => Code = code;
        
        public static implicit operator bool(ExitCode code)
            => code.Code == 0;
        
        public static implicit operator ExitCode(int code)
            => new ExitCode(code);
        
        public static implicit operator int(ExitCode code)
            => code.Code;
        
        public override string ToString() 
            => $"Exit Code: {Code}";
        
        public void ThrowIfNonZero(System.Exception exception)
        {
            if(!(bool)this)
                throw exception;
        }
        
        public void ThrowIfNonZero(System.Func<ExitCode, System.Exception> exception)
        {
            if(!(bool)this)
                throw exception(this);
        }
        
        public void ThrowIf(int code, System.Exception exception)
        {
            if(Code == code)
                throw exception;
        }
        
        public void ThrowIf(int code, System.Func<ExitCode, System.Exception> exception)
        {
            if(Code == code)
                throw exception(this);
        }
        
        public void ThrowIfNon(int code, System.Exception exception)
        {
            if(Code != code)
                throw exception;
        }
        
        public void ThrowIfNon(int code, System.Func<ExitCode, System.Exception> exception)
        {
            if(Code != code)
                throw exception(this);
        }
    }
}
