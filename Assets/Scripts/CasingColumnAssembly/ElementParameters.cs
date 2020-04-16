using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CasingColumnAssembly
{
    /// <summary>
    /// параметры элемента колонны
    /// </summary>
    public class ElementParameters
    {
        private const string NO_DATA = "нет данных";
        private const string NOT_CORRECT_DATA = "не корректные данные";

        public string name { get; private set; }
        public string id { get; private set; }
        public string top { get; private set; }
        public string bottom { get; private set; }
        public string length { get; private set; }
        public string diameter { get; private set; }
        public string casingKind { get; private set; }
        public string pipeModel { get; private set; }


        public ElementParameters(string name, string top, string bottom, string length, string diameter, string casingKind)
        {
            this.name = string.IsNullOrEmpty(name) ? NO_DATA : name;
            this.top = CheckFloat(top);
            this.bottom = CheckFloat(bottom);
            this.length = CheckFloat(length);
            this.diameter = CheckFloat(diameter);
            this.casingKind = string.IsNullOrEmpty(casingKind) ? NO_DATA : casingKind;

            // Debug.Log($" ElementParameters - name {name} top {top} bottom {bottom} length {length} diameter {diameter} casingKind {casingKind}");
        }

        /// <summary>
        /// проверяет входные параметры типа float на заполненность
        /// </summary>
        private string CheckFloat(string parameter)
        {
            string rezult = parameter;
            // значение, если параметр типа float
            float value = 0;

            bool nullOrEmpty = string.IsNullOrEmpty(parameter);

            bool nullOrSpace = string.IsNullOrWhiteSpace(parameter);

            //bool isFloat = float.TryParse(parameter, out value);
            bool isFloat = float.TryParse(parameter, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out value);

            if (nullOrEmpty || nullOrSpace) // если не задана, пусто или пробел
            {
                rezult = NO_DATA;
            }
            else if (isFloat)
            {
                // if (value == 0) // если float, но равно нулю
                //   rezult = NO_DATA;
            }
            else
                rezult = NOT_CORRECT_DATA;

            return rezult;
        }

        /// <summary>
        /// проверяет входные параметры типа string на заполненность
        /// </summary>
        private string CheckString(string parameter)
        {
            string rezult = parameter;

            bool nullOrEmpty = string.IsNullOrEmpty(parameter);

            bool nullOrSpace = string.IsNullOrWhiteSpace(parameter);

            if (nullOrEmpty || nullOrSpace) // если не задана, пусто или пробел
                rezult = NO_DATA;

            return rezult;
        }
    }
    /*
     "casingString": [
        {
          "id": "0085f6ff-664d-4c72-84a3-b0dbfb273121",
          "name": "Кондуктор",
          "top": 0,
          "bottom": 585,
          "length": 585,
          "diameter": 0.3239,
          "casingKind": "Кондуктор"
        },
        {
          "id": "68006d31-f0bc-443e-b013-b2a7defac761",
          "name": "Техническая колонна 1",
          "top": 0,
          "bottom": 1610,
          "length": 1610,
          "diameter": 0,
          "casingKind": "Техническая колонна 1"
        },
        {
          "id": "a8bddd48-98ae-4f6d-ad29-db65fdece674",
          "name": "Эксплуатационная колонна",
          "top": 0,
          "bottom": 1836.45,
          "length": 1836.45,
          "diameter": 0,
          "casingKind": "Эксплуатационная колонна"
        },
        {
          "id": "4951c05e-ec6a-46b9-82e3-f74d1931ff12",
          "name": "Направление",
          "top": 0,
          "bottom": 58,
          "length": 58,
          "diameter": 0.426,
          "casingKind": "Направление"
        }
      ]
     */
}
