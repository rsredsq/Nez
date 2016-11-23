using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//в один тик запускает всех детей. возвращает false, если один из детей != true

namespace Nez.AI.BehaviorTrees
{
    class ParallelAll<T> : Composite<T>
    {
        public override TaskStatus update(T context)
        {
            TaskStatus ans = TaskStatus.Success;

            var didAllFail = true;
            for (var i = 0; i < _children.Count; i++)
            {
                var child = _children[i];
                child.tick(context);

                // if any child succeeds we return success
                if (child.status != TaskStatus.Success)
                    ans = TaskStatus.Failure;

                // if all children didn't fail, we're not done yet
                if (child.status != TaskStatus.Failure)
                    didAllFail = false;
            }

            if (didAllFail) return TaskStatus.Failure;

            return ans;
        }
    }
}
