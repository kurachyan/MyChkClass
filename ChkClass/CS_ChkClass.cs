using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rsvwrd;

namespace ChkClass
{
    public class CS_ChkClass
    {
        #region 共有領域
//        CS_LRskip lrskip;           // 両側余白情報を削除
        CS_Rsvwrd rsvwrd;           // 予約語を確認する

        private String _wbuf;
        private Boolean _empty;
        public String Wbuf
        {
            get
            {
                return (_wbuf);
            }
            set
            {
                _wbuf = value;
                if (_wbuf == null)
                {   // 設定情報は無し？
                    _empty = true;
                }
                else
                {   // 整形処理を行う
/*
                    if (lrskip == null)
                    {   // 未定義？
                        lrskip = new CS_LRskip();
                    }
                    lrskip.Exec(_wbuf);
                    _wbuf = lrskip.Wbuf;
*/
                    // 作業の為の下処理
                    if (_wbuf.Length == 0 || _wbuf == null)
                    {   // バッファー情報無し
                        // _wbuf = null;
                        _empty = true;
                    }
                    else
                    {
                        _empty = false;
                    }
                }
            }
        }
        private String _result;     // [Namespace]ＬＢＬ情報
        public String Result
        {
            get
            {
                return (_result);
            }
            set
            {
                _result = value;
            }
        }

        // 予約語：「ＮａｍｅＳｐａｃｅ」
        const int RSV_NONE = 0;     // 未定義
        const int RSV_OTHER = 1;    // 予約語１：クラス
                                    //        const string RSV_KEYWORD = "namespace";
                                    //        private int _rsvcode;

        private static Boolean _Is_class;
        public Boolean Is_class
        {
            get
            {
                return (_Is_class);
            }
            set
            {
                _Is_class = value;
            }
        }
        #endregion

        #region コンストラクタ
        public CS_ChkClass()
        {   // コンストラクタ
            _wbuf = null;       // 設定情報無し
            _empty = true;

            //            _rsvcode = RSV_NONE;    // 予約語：未定義
            _Is_class = false;  // [class]フラグ：false

            rsvwrd = new CS_Rsvwrd();           // 予約語を確認する
        }
        #endregion

        #region モジュール
        public void Clear()
        {   // 作業領域の初期化
            _wbuf = null;       // 設定情報無し
            _empty = true;

            //            _rsvcode = RSV_NONE;    // 予約語：未定義
            _Is_class = false;  // [class]フラグ：false
        }

        public void Exec()
        {   // "class"評価
            if (!_empty)
            {   // バッファーに実装有り
                rsvwrd.Exec(_wbuf);     // 評価情報の予約語確認を行う

                if (_Is_class)
                {   // [class]フラグは、true？
                    if (!rsvwrd.Is_class)
                    {   // 評価情報は、非予約語？
                        // ＬＢＬ情報テーブルに、class名を登録する
                        _Is_class = false;       // [class]フラグ：false
                    }
                }
                else
                {   // [class]フラグは、false
                    if (rsvwrd.Is_class)
                    {   // 評価情報は、"class"？
                        _Is_class = true;       // [class]フラグ：true
                        rsvwrd.Is_class = false;
                    }
                }
            }
        }
        #endregion
    }
}
