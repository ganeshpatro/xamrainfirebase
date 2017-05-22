﻿using System;

using Foundation;
using UIKit;
using FirebaseXamarin.Core.Utils;

namespace FirebaseXamarin.iOS.Cells
{
    public partial class ChatRoomsCell : UITableViewCell
    {
        public static readonly NSString Key = new NSString("ChatRoomsCell");
        public static readonly UINib Nib;

        static ChatRoomsCell()
        {
            Nib = UINib.FromName("ChatRoomsCell", NSBundle.MainBundle);
        }

        protected ChatRoomsCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void AwakeFromNib()
        {
            lblMessageCount.Layer.CornerRadius = (float)12.5;
            lblMessageCount.ClipsToBounds = true;
        }

        public void populateData(RoomsMetaData roomMetaData)
        {
            lblDateTime.Text = Utils.getFormmatedTime(roomMetaData.createdTime).ToString(Constants.DISPLAY_DATE_FORMAT);
            if (!String.IsNullOrEmpty(roomMetaData.displayName))
            {
                lblChatRoomName.Text = roomMetaData.displayName;
            }
            else
            {
                lblChatRoomName.Text = "";
                if (roomMetaData.users != null && roomMetaData.users.Count > 0)
                {
                    string otherUserId = roomMetaData.users.Find(x => x != DBManager.sharedManager.getLoggedInUserInfo().uid);
                    FirebaseManager.sharedManager.getUserInfo(otherUserId, (User userInfo) =>
                                    {
                                        lblChatRoomName.Text = userInfo.name;
                                    });
                }
            }
        }

    }
}
