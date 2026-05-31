using System;
using System.Collections.Generic;
using System.Text;

namespace Arta.Base.Core.Validators
{
    public interface IValidator<TModel>
    {
        void Validate(TModel model);
    }

}
