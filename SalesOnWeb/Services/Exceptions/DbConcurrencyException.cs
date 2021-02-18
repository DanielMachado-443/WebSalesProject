using System;

namespace SalesOnWeb.Services.Exceptions {
    public class DbConcurrencyException : ApplicationException {
        public DbConcurrencyException(string message) : base(message) {
        }
    }
}
