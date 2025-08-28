using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Domain.DTOs
{
    public class ResponseModel
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }

        public ResponseModel() { }

        public ResponseModel(object? data, string message = "", bool success = true)
        {
            Data = data;
            Message = message;
            Success = success;
        }
    }

    // Generic version if you still want strong typing elsewhere
    public class ResponseModel<T> : ResponseModel
    {
        public new T? Data
        {
            get => (T?)base.Data;
            set => base.Data = value;
        }

        public ResponseModel() { }

        public ResponseModel(T? data, string message = "", bool success = true)
            : base(data, message, success)
        {
        }
    }

}
