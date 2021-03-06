﻿using System;
using System.Reactive.Disposables;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using Prism.Services.Dialogs.Extensions;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;

namespace PrismNetCoreApp
{
	/// <summary>アプリケーションのメイン画面を表します。</summary>
	public class MainWindowViewModel : BindableBase, IDisposable
	{
		#region プロパティ

		/// <summary>Windowタイトルを取得します。</summary>
		public ReactivePropertySlim<string> Title { get; }

		/// <summary>WindowがClose可能かを取得します。</summary>
		public ReactivePropertySlim<bool> CanClose { get; }

		#endregion

		#region メソッド

		/// <summary>WindowのCloseを確認します。</summary>
		public void ConfirmClose()
			=> this.CanClose.Value = this.dialogService.ShowConfirmationMessage("ウィンドウを閉じますか？") == ButtonResult.Yes;

		#endregion

		#region IDisposable Support

		private bool disposedValue = false; // 重複する呼び出しを検出するには

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					this.disposables.Dispose();

					foreach (var region in this.regionManager.Regions)
					{
						region.RemoveAll();
					}
				}

				// TODO: アンマネージ リソース (アンマネージ オブジェクト) を解放し、下のファイナライザーをオーバーライドします。
				// TODO: 大きなフィールドを null に設定します。

				disposedValue = true;
			}
		}

		// TODO: 上の Dispose(bool disposing) にアンマネージ リソースを解放するコードが含まれる場合にのみ、ファイナライザーをオーバーライドします。
		// ~MainWindowViewModel()
		// {
		//   // このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
		//   Dispose(false);
		// }

		// このコードは、破棄可能なパターンを正しく実装できるように追加されました。
		public void Dispose()
		{
			// このコードを変更しないでください。クリーンアップ コードを上の Dispose(bool disposing) に記述します。
			Dispose(true);
			// TODO: 上のファイナライザーがオーバーライドされる場合は、次の行のコメントを解除してください。
			// GC.SuppressFinalize(this);
		}

		#endregion

		#region コンストラクタ

		private IRegionManager regionManager = null;
		private IDialogService dialogService = null;

		private CompositeDisposable disposables = new CompositeDisposable();

		/// <summary>コンストラクタ。</summary>
		public MainWindowViewModel(IRegionManager regionMan, IDialogService dlgService)
		{
			this.regionManager = regionMan;
			this.dialogService = dlgService;

			this.Title = new ReactivePropertySlim<string>(".NET Core 3.0 Application")
				.AddTo(this.disposables);
			this.CanClose = new ReactivePropertySlim<bool>(false)
				.AddTo(this.disposables);
		}

		#endregion
	}
}
