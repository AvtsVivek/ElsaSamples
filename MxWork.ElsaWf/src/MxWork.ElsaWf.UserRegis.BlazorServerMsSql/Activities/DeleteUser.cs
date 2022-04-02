using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Results;
using Elsa.Services;
using Elsa.Services.Models;
using MxWork.ElsaWf.UserRegis.BlazorServerMsSql.Models;
using Elsa;

namespace MxWork.ElsaWf.UserRegis.BlazorServerMsSql.Activities
{
    [ActivityDefinition(Category = "Users", Description = "Delete a User", Icon = "fas fa-user-minus", Outcomes = new[] { OutcomeNames.Done, "Not Found" })]
    public class DeleteUser : Activity
    {
        private readonly IMongoCollection<User> _store;

        public DeleteUser(IMongoCollection<User> store)
        {
            _store = store;
        }

        [ActivityProperty(Hint = "Enter an expression that evaluates to the ID of the user to activate.")]
        public WorkflowExpression<string> UserId
        {
            get => GetState<WorkflowExpression<string>>();
            set => SetState(value);
        }

        protected override async Task<ActivityExecutionResult> OnExecuteAsync(WorkflowExecutionContext context, CancellationToken cancellationToken)
        {
            var userId = await context.EvaluateAsync(UserId, cancellationToken);
            var result = await _store.DeleteOneAsync(x => x.Id == userId, cancellationToken);

            return result.DeletedCount == 0 ? Outcome("Not Found") : Done();
        }
    }
}
