using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Client.Models
{
	public class LoginModel
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
		public string IP { get; set; }
		public string UserAgent { get; set; }
		public string ExternalAuthenticator { get; set; }       // Google, Facebook, Microsoft, ... 
		public string ExternalAuthenticatorData { get; set; }       // Json , or seperated string of values..
	}

	// used by the FluentValidation thingy
	public class LoginModelValidator : AbstractValidator<LoginModel>
	{
		public LoginModelValidator()
		{
			// set the rules...
			RuleFor(p => p.Username).NotEmpty().WithMessage("You must enter a name");
			RuleFor(p => p.Username).MinimumLength(6).MaximumLength(50).WithMessage("Name must be betweem 6 and 50 characters");
			RuleFor(p => p.Password).NotEmpty().WithMessage("Password must enter a password");
			RuleFor(p => p.Password).MinimumLength(6).MaximumLength(50).WithMessage("Password must be between 6 and 50 chars");
			//RuleFor(p => p.EmailAddress).NotEmpty().WithMessage("You must enter a email address");
			//RuleFor(p => p.EmailAddress).EmailAddress().WithMessage("You must provide a valid email address");

			// just some test now..
			//RuleFor(x => x.Username).MustAsync(async (name, cancellationToken) => await IsUniqueAsync(name))
			//					.WithMessage("Name must be unique")
			//					.When(person => !string.IsNullOrEmpty(person.Username));
		}

		private async Task<bool> IsUniqueAsync(string name)
		{
			// TODO: this should really be calling the user service to check if usernam/email is available
			await Task.Delay(300);
			return name.ToLower() != "test";
		}
	}
}
