using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace RssReader {
    public class SettingData {
#pragma warning disable CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。'required' 修飾子を追加するか、Null 許容として宣言することを検討してください。
        private static SettingData instance;
#pragma warning restore CS8618 // null 非許容のフィールドには、コンストラクターの終了時に null 以外の値が入っていなければなりません。'required' 修飾子を追加するか、Null 許容として宣言することを検討してください。
        private Sdata data;
        private const string FILE_PATH = "setting.json";
        public static SettingData GetInstance() {
            if (instance == null) {
                instance = new SettingData();
            }
            return instance;
        }

        private SettingData() {
            data = StaticEvent.LoadItem<Sdata>(FILE_PATH) ?? new Sdata();
        }

        public void SaveItem(Sdata d) {
            StaticEvent.SaveItem(FILE_PATH, d);
            data = StaticEvent.LoadItem<Sdata>(FILE_PATH) ?? new Sdata();
        }


        public Color[] GetBackColor() {
            return [Color.FromArgb(data.OddBackColor), Color.FromArgb(data.EvenBackColor), Color.FromArgb(data.BackBackColor), Color.FromArgb(data.TextBackColor)];
        }

        public int GetTimeOutValue() {
            return data.TimeOutValue;
        }

 

    }
}
