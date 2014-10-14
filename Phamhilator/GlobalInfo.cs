﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Controls;



namespace Phamhilator
{
	public static class GlobalInfo
	{
		public static readonly QuestionFilters.WhiteFilters.BadUsername QWhiteName = new QuestionFilters.WhiteFilters.BadUsername();
		public static readonly QuestionFilters.WhiteFilters.Offensive QWhiteOff = new QuestionFilters.WhiteFilters.Offensive();
		public static readonly QuestionFilters.WhiteFilters.Spam QWhiteSpam = new QuestionFilters.WhiteFilters.Spam();
		public static readonly QuestionFilters.WhiteFilters.LQ QWhiteLQ = new QuestionFilters.WhiteFilters.LQ();

		public static readonly QuestionFilters.BlackFilters.BadUsername QBlackName = new QuestionFilters.BlackFilters.BadUsername();
		public static readonly QuestionFilters.BlackFilters.Offensive QBlackOff = new QuestionFilters.BlackFilters.Offensive();
		public static readonly QuestionFilters.BlackFilters.Spam QBlackSpam = new QuestionFilters.BlackFilters.Spam();
		public static readonly QuestionFilters.BlackFilters.LQ QBlackLQ = new QuestionFilters.BlackFilters.LQ();


		public static readonly AnswerFilters.BlackFilters.BadUsername ABlackName = new AnswerFilters.BlackFilters.BadUsername();
		public static readonly AnswerFilters.BlackFilters.Offensive ABlackOff = new AnswerFilters.BlackFilters.Offensive();
		public static readonly AnswerFilters.BlackFilters.Spam ABlackSpam = new AnswerFilters.BlackFilters.Spam();
		public static readonly AnswerFilters.BlackFilters.LQ ABlackLQ = new AnswerFilters.BlackFilters.LQ();

		public static readonly AnswerFilters.WhiteFilters.BadUsername AWhiteName = new AnswerFilters.WhiteFilters.BadUsername();
		public static readonly AnswerFilters.WhiteFilters.Offensive AWhiteOff = new AnswerFilters.WhiteFilters.Offensive();
		public static readonly AnswerFilters.WhiteFilters.Spam AWhiteSpam = new AnswerFilters.WhiteFilters.Spam();
		public static readonly AnswerFilters.WhiteFilters.LQ AWhiteLQ = new AnswerFilters.WhiteFilters.LQ();


		public const string BotUsername = "pham"; // TODO: change this to the username of your account which the bot will be using.
		public static readonly Dictionary<int, MessageInfo> PostedReports = new Dictionary<int, MessageInfo>(); // Message ID, actual message.
		public static readonly MessageHandler MessagePoster = new MessageHandler();
		public const string Owners = "Sam, Unihedron & ProgramFOX";
		public static WebBrowser ChatWb;
		public static WebBrowser AnnounceWb;
		public static int PostsCaught;
		public static DateTime UpTime;
		public static bool BotRunning;
		public static bool Exit;

		public static bool EnableFullScan
		{
			get
			{
				return Boolean.Parse(File.ReadAllText(DirectoryTools.GetEnableFullScanFile()));
			}

			set
			{
				File.WriteAllText(DirectoryTools.GetEnableFullScanFile(), value.ToString(CultureInfo.InvariantCulture));
			}
		}

		public static float AccuracyThreshold
		{
			get
			{
				return Single.Parse(File.ReadAllText(DirectoryTools.GetAccuracyThresholdFile()));
			}

			set
			{
				File.WriteAllText(DirectoryTools.GetAccuracyThresholdFile(), value.ToString(CultureInfo.InvariantCulture));
			}
		}

		public static int TermCount
		{
			get
			{
				return QBlackOff.Terms.Count + QBlackSpam.Terms.Count + QBlackLQ.Terms.Count + QBlackName.Terms.Count + 
					   ABlackOff.Terms.Count + ABlackSpam.Terms.Count + ABlackLQ.Terms.Count + ABlackName.Terms.Count +
					   QWhiteName.Terms.Values.Sum(x => x.Count) + QWhiteOff.Terms.Values.Sum(x => x.Count) + QWhiteSpam.Terms.Values.Sum(x => x.Count) + QWhiteLQ.Terms.Values.Sum(x => x.Count) +
					   AWhiteName.Terms.Values.Sum(x => x.Count) + AWhiteOff.Terms.Values.Sum(x => x.Count) + AWhiteSpam.Terms.Values.Sum(x => x.Count) + AWhiteLQ.Terms.Values.Sum(x => x.Count) + 
					   BadTagDefinitions.BadTags.Count;
			}
		}

		public static int ChatRoomID
		{
			get
			{
				var ID = 0;

				App.Current.Dispatcher.Invoke(() =>
				{
					try
					{
						var startIndex = ChatWb.Source.AbsolutePath.IndexOf("rooms/", StringComparison.Ordinal) + 6;
						var endIndex = ChatWb.Source.AbsolutePath.IndexOf("/", startIndex + 1, StringComparison.Ordinal);

						var IDString = ChatWb.Source.AbsolutePath.Substring(startIndex, endIndex - startIndex);

						if (!IDString.All(Char.IsDigit)) { return; }

						ID = int.Parse(IDString);
					}
					catch (Exception)
					{

					}
				});

				return ID;
			}
		}

		public static int AnnouncerRoomID
		{
			get
			{
				var ID = 0;

				App.Current.Dispatcher.Invoke(() =>
				{
					try
					{
						var startIndex = AnnounceWb.Source.AbsolutePath.IndexOf("rooms/", StringComparison.Ordinal) + 6;
						var endIndex = AnnounceWb.Source.AbsolutePath.IndexOf("/", startIndex + 1, StringComparison.Ordinal);

						var IDString = AnnounceWb.Source.AbsolutePath.Substring(startIndex, endIndex - startIndex);

						if (!IDString.All(Char.IsDigit)) { return; }

						ID = int.Parse(IDString);
					}
					catch (Exception)
					{

					}
				});

				return ID;
			}
		}
	}
}
