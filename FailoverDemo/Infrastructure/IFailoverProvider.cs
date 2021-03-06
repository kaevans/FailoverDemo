﻿namespace FailoverDemo.Infrastructure
{
    public interface IFailoverProvider
    {        
        bool ShouldFailover { get; set; }
        string GetConnectionString();

        bool? IsReadOnly { get; set; }
    }
}
