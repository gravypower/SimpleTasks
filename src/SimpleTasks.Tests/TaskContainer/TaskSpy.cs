using System.Collections.Generic;

namespace SimpleTasks.Tests.TaskContainer
{
    public class TaskSpy
    {
        private readonly IList<string> _orderList;
        private readonly string _order;
        public bool Called { get; set; }

        public TaskSpy()
        {
            _orderList = new List<string>();
        }

        public TaskSpy(IList<string> orderList, string order)
        {
            _orderList = orderList;
            _order = order;
        }

        public void Run()
        {
            _orderList.Add(_order);
            Called = true;
        }
    }
}
