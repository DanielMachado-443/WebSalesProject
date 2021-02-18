using System;

namespace SalesOnWeb.Services.Exceptions {
    public class NotFoundException : ApplicationException {
        public NotFoundException(string message) : base(message) {
        }
    }
}
