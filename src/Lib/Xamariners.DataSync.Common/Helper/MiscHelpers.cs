using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Xamariners.DataSync.Common.Helper
{
    public static class MiscHelpers
    {

        public static void ThrowIfNull<T>(Expression<Func<T>> propertyExpression, string message = null)
        {
            MemberExpression body;
            body = propertyExpression.Body as MemberExpression;

            if (body == null)
                body = (propertyExpression.Body as UnaryExpression).Operand as MemberExpression;

            T value = propertyExpression.Compile().Invoke();

            if (value == null)
            {
                if (string.IsNullOrEmpty(message))
                    throw new ArgumentNullException(body.Member.Name);
                else
                    throw new ArgumentNullException(body.Member.Name, message);
            }
        }

        public static void ThrowIfNull(params Expression<Func<object>>[] propertyExpressions)
        {
            foreach (var propertyExpression in propertyExpressions)
            {
                ThrowIfNull<object>(propertyExpression);
            }
        }


        public static Task StartNewCancellableTask(Action mainAction, Action prerequisiteAction,
            CancellationTokenSource cts, object padlock, int refreshRate, int executeFraction = 10)
        {
            return Task.Run(new Action(async () =>
            {
                prerequisiteAction();

                //initial counter. we want to start right now
                var counter = executeFraction - 1;

                while (cts == null || !cts.IsCancellationRequested)
                {
                    counter++;

                    if (counter % executeFraction == 0)
                    {
                        counter = 0;
                        mainAction();
                    }

                    await Task.Delay((refreshRate / executeFraction) * 1000);
                }
            }));
        }
    }
}
