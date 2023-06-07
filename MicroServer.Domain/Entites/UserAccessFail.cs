using System;
namespace MicroServer.Domain.Entites
{
    public class UserAccessFail
    {
        public Guid Id { get; init; }
        public User User { get; init; }
        public Guid UserId { get; init; }
        private bool lockOut;

        public DateTime? LockoutEnd { get; private set; }
        public int AccessFailCount { get; private set; }

        private UserAccessFail() { }

        public UserAccessFail(User user)
        {
            this.Id = Guid.NewGuid();
            this.User = user;
        }

        public void Reset()
        {
            this.AccessFailCount = 0;
            this.LockoutEnd = null;
            this.lockOut = false;
        }

        public void Fail()
        {
            this.AccessFailCount++;
            if (this.AccessFailCount > 3)
            {
                this.LockoutEnd = DateTime.Now.AddMinutes(5);
                this.lockOut = true;
            }
        }

        public bool IsLockOut()
        {
            if (this.lockOut)
            {
                if (DateTime.Now > this.LockoutEnd)
                {
                    Reset();
                    return false;
                }

                return true;
            }

            return false;


        }
    }
}

