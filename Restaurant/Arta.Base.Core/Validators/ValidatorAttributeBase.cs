using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Arta.Base.Core.Validators
{
    public abstract partial class ValidatorAttributeBase
    {
        private readonly string _helpParameterName;
        private Func<object, bool> _validate = default!;

        protected ValidatorAttributeBase(string helpParameterName)
        {
            _helpParameterName = helpParameterName;
        }

        public Func<object, bool> Validate { get => _validate; set => _validate = value; }
    }
}
