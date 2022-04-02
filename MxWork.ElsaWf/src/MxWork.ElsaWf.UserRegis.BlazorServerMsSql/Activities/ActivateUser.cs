using System.Threading;
using System.Threading.Tasks;
using Elsa.Attributes;
using Elsa.Expressions;
using Elsa.Results;
using MxWork.ElsaWf.UserRegis.BlazorServerMsSql.Models;
using Elsa.Services;
using Elsa.Services.Models;
using Elsa;
using Elsa.Persistence.EntityFrameworkCore.DbContexts;

namespace MxWork.ElsaWf.UserRegis.BlazorServerMsSql.Activities
{
    [ActivityDefinition(Category = "Users", Description = "Activate a User", Icon = "fas fa-user-check", Outcomes = new[] { OutcomeNames.Done, "Not Found" })]
    public class ActivateUser : Activity
    {
        private readonly IMongoCollection<User> _store;
        private readonly SqlServerContext _sqlServerContext;

        public ActivateUser(IMongoCollection<User> store, SqlServerContext sqlServerContext)
        {
            _store = store;
            _sqlServerContext = sqlServerContext;
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
            var  = await _store.AsQueryable().FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            if (user == null)
                return Outcome("Not Found");

            user.IsActive = true;
            await _store.ReplaceOneAsync(x => x.Id == userId, user, cancellationToken: cancellationToken);

            return Done();
        }
    }
}
