using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrecosQuestionnaire.Models
{
    public class SharedUnitModel
    {
        private string _sharedRoomCode;
        private string _sharedRoomName;

        public int Id { get; set; }
        public string SharedRoomCode
        {
            get
            {
                return _sharedRoomCode;
            }
            set
            {
                _sharedRoomCode = value;
            }
        }
        public string SharedRoomName
        {
            get
            {
                return _sharedRoomName;
            }
            set
            {
                _sharedRoomName = value;
            }
        }

        public int MainRoomModelId { get; set; }

        public SharedUnitModel(string sharedRoomCode, string sharedRoomName)
        {
            _sharedRoomCode = sharedRoomCode;
            _sharedRoomName = sharedRoomName;
        }

        public SharedUnitModel()
        {

        }
    }
}
