using System;

namespace myFunction {
    public class Function {

        #region function
        public int RandomNumber(int selectedDice) {
            Random myRand = new Random(Guid.NewGuid().GetHashCode());
            int randomNumber = (myRand.Next(1, selectedDice + 1));
            return randomNumber;
        }

        public int TimesDiceSided(int Times,int sided_dice) {
            int totalRandomNumber = 0;
            for (int i = 1; i <= Times; i++) {
                totalRandomNumber += RandomNumber(sided_dice);
            }
            return totalRandomNumber;
        }
        #endregion

        #region sortFunction
        public int[] SortTheArray(int[] intArrray, bool isbigtoSmall) {
            int saveNum = 0;
            for (int i = 0; i <= intArrray.Length - 1; i++) {
                for (int j = 0; j <= intArrray.Length - 2; j++) {
                    if (isbigtoSmall) {
                        if (intArrray[j] < intArrray[j + 1]) {
                            saveNum = intArrray[j + 1];
                            intArrray[j + 1] = intArrray[j];
                            intArrray[j] = saveNum;
                        }
                    }
                    else {
                        if (intArrray[j] > intArrray[j + 1]) {
                            saveNum = intArrray[j + 1];
                            intArrray[j + 1] = intArrray[j];
                            intArrray[j] = saveNum;
                        }
                    }

                }
            }
            return intArrray;
        }

        public string[] SortTheArray(string[] stringArray, bool isbigtoSmall) {
            Array.Sort(stringArray, StringComparer.InvariantCulture);
            if (!isbigtoSmall) {
                string saveString = "";
                //Array.Reverse(stringArray);
                for (int i = 0; i <= (stringArray.Length - 1) /2 ; i++) {
                    saveString = stringArray[stringArray.Length - 1 - i ];
                    stringArray[stringArray.Length - 1 -i ] = stringArray[i];
                    stringArray[i] = saveString;
                }
            }
            return stringArray;
        }
        #endregion

        #region keysFunction
        public string keysStringConvert(string keysString) {
            switch (keysString) {
                case "OemOpenBrackets":
                    return "[";
                case "Oem6":
                    return "]";
                case "Oem1":
                    return ";";
                case "Oem7":
                    return "'";
                case "Oemcomma":
                    return ",";
                case "OemPeriod":
                    return ".";
                case "OemQuestion":
                    return "/";
                case "OemMinus":
                    return "-";
                case "OemPlus":
                    return "=";
                case "Oem5":
                    return "|";
                case "Next":
                    return "PageDown";

                default:
                    return keysString;
            }
        }
        #endregion
    }
}
