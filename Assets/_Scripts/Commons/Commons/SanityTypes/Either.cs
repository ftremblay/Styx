using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Commons.Commons.SanityTypes
{
    public class Either<TL, TR>
    {
        private readonly TL left;
        private readonly TR right;
        private readonly bool isLeft;

        public Either(TL left)
        {
            this.left = left;
            this.isLeft = true;
        }

        public Either(TR right)
        {
            this.right = right;
            this.isLeft = false;
        }

        public T Match<T>(Func<TL, T> leftFunc, Func<TR, T> rightFunc)
        {
            if (leftFunc == null)
            {
                throw new ArgumentNullException("leftFunc");
            }

            if (rightFunc == null)
            {
                throw new ArgumentNullException("rightFunc");
            }

            return this.isLeft ? leftFunc(this.left) : rightFunc(this.right);
        }

        public void Match(Action<TL> leftFunc, Action<TR> rightFunc)
        {
            if (leftFunc == null)
            {
                throw new ArgumentNullException("leftFunc");
            }

            if (rightFunc == null)
            {
                throw new ArgumentNullException("rightFunc");
            }

            if (this.isLeft)
                leftFunc(this.left);
            else
                rightFunc(this.right);
        }

        /// <summary>
        /// If right value is assigned, execute an action on it.
        /// </summary>
        /// <param name="rightAction">Action to execute.</param>
        public void DoRight(Action<TR> rightAction)
        {
            if (rightAction == null)
            {
                throw new ArgumentNullException("rightAction");
            }

            if (!this.isLeft)
            {
                rightAction(this.right);
            }
        }
        
        public TL LeftOrDefault()
        {
            return Match(l => l, r => default(TL));
        }

        public TR RightOrDefault()
        {
            return Match(l => default(TR), r => r);
        }

        public static implicit operator Either<TL, TR>(TL left)
        {
            return new Either<TL, TR>(left);
        }

        public static implicit operator Either<TL, TR>(TR right)
        {
            return new Either<TL, TR>(right);
        }
    }
}
