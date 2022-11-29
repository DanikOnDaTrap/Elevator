using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elevator
{
    public enum Move
    {
        Up,
        Down
    }

    public enum Status
    {
        Opened,
        Closed
    }

    class Lift
    {
        private Move _move;
        public Move DirMove
        {
            get { return _move; }
            set { _move = value; }
        }

        private Status _status;
        public Status DoorsStatus
        {
            get { return _status; }
            set { _status = value; }
        }

        private int _countFloors;
        public int CountFloor
        {
            get { return _countFloors; }
            set { _countFloors = value; }
        }

        private int _currFloor;
        public int CurrFloor
        {
            get { return _currFloor; }
            set { _currFloor = value; }
        }

        public Lift()
        {
            _move = Elevator.Move.Up;
            _status = Status.Opened;
            _countFloors = 0;
            _currFloor = 1;
        }

        public void MoveUp()
        {
            if (_currFloor < _countFloors)
            {
                _move = Move.Up;
                _currFloor += 1;
            }
            else
            {
                _move = Move.Down;
            }
        }

        public void MoveDown()
        {
            if (_currFloor > 1)
            {
                _move = Move.Down;
                _currFloor -= 1;
            }
            else
            {
                _move = Move.Up;
            }
        }
    }
}
