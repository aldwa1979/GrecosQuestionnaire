using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Models
{
    public class MainRoomModel
    {
        private string _mainRoomCode;
        private List<SharedUnitModel> _sharedUnit;

        public int Id { get; set; }

        public string MainRoomCode
        {
            get
            {
                return _mainRoomCode;
            }
            set
            {
                _mainRoomCode = value;
            }
        }

        public int HotelModelId { get; set; }
        public List<SharedUnitModel> SharedUnit
        {
            get
            {
                return _sharedUnit;
            }
            set
            {
                _sharedUnit = value;
            }
        }

        public MainRoomModel(string mainRoomCode, List<SharedUnitModel> sharedUnit)
        {
            _mainRoomCode = mainRoomCode;
            _sharedUnit = sharedUnit;
        }

        public MainRoomModel()
        {
        }
    }
}
