using Presenters.ChapterSelect;
using SceneManagers.Parameters;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace SceneManagers {

	/// <summary>
	/// シーンの管理
	/// </summary>
	public class SceneManager {

		/// <summary>
		/// インスタンス
		/// </summary>
		private static SceneManager Instance;
		
		/// <summary>
		/// インスタンス取得
		/// </summary>
		/// <returns>SceneManager</returns>
		public static SceneManager GetInstance() {
			Logger.Debug( "Start" );
			if( Instance == null ) {
				Logger.Debug( "Instance is Null" );
				Instance = new SceneManager();
			}
			Logger.Debug( "End" );
			return Instance;
		}

		/// <summary>
		/// 画面遷移時に渡すパラメータ
		/// </summary>
		private static object Parameter;

		/// <summary>
		/// シングルモードで遷移した場合の前シーンの名前
		/// </summary>
		public static string BeforeSingleModeSceneName {
			private set;
			get;
		}

		/// <summary>
		/// シングルモードで遷移した場合の現在のシーン名
		/// </summary>
		public static string CurrentSingleModeSceneName {
			private set;
			get;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		private SceneManager() {
			Logger.Debug( "Start" );

			// シーン切り替え時イベント追加
			UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;

			new ChapterSelectPresenter( new ChapterSelectParameter() {
				SaveDataId = 0 ,
				IsSinglePlayMode = true ,
				ClearedChapters = new List<ChapterSelectParameter.Chapter>() {
					new ChapterSelectParameter.Chapter() {
						Id = 0
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 1
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 2
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 3
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 4
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 5
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 6
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 7
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 8
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 9
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 10
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 11
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 12
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 13
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 14
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 15
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 16
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 17
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 18
					} ,
					new ChapterSelectParameter.Chapter() {
						Id = 19
					}
				}
			} );

			Logger.Debug( "End" );
		}

		/// <summary>
		/// シーン読み込み
		/// </summary>
		/// <param name="sceneName">読み込みシーン名</param>
		/// <param name="parameter">遷移先シーンに渡すパラメータ</param>
		public void LoadScene( string sceneName , object parameter ) {
			Logger.Debug( "Start" );
			Parameter = parameter;
			UnityEngine.SceneManagement.SceneManager.LoadScene( sceneName );
			Logger.Debug( "End" );
		}

		/// <summary>
		/// 現在のシーン名が指定のシーン名と一致するかどうか
		/// </summary>
		/// <param name="sceneName">シーン名</param>
		/// <returns>現在のシーン名が指定のシーン名と一致するかどうか</returns>
		private static bool EqualsActiveSceneName( string sceneName ) {
			Logger.Debug( "Start" );
			Logger.Debug( $"Scene Name is {sceneName}" );
			Logger.Debug( "End" );
			return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name.Equals( sceneName );
		}

		/// <summary>
		/// シーン切り替え時イベント
		/// </summary>
		/// <param name="scene">遷移後シーン</param>
		/// <param name="loadSceneMode">遷移モード</param>
		private static void OnSceneLoaded(
			Scene scene ,
			LoadSceneMode loadSceneMode
		) {
			Logger.Debug( "Start" );

			Logger.Debug( $"Scene Name is {scene.name}" );
			Logger.Debug( $"Load Scene Mode is {loadSceneMode}" );

			// シングルモードで遷移した場合は前シーンの名前を保持しておく
			if( loadSceneMode == LoadSceneMode.Single ) {
				BeforeSingleModeSceneName = CurrentSingleModeSceneName;
				CurrentSingleModeSceneName = scene.name;
			}

			Logger.Debug( $"Before Single Mode Scene Name is {BeforeSingleModeSceneName}" );
			Logger.Debug( $"Current Single Mode Scene Name is {CurrentSingleModeSceneName}" );

			// シーン切り替え時にはおおもとになるPresenterのインスタンスを生成
			switch( scene.name ) {
				case "ChapterSelect":
					new ChapterSelectPresenter( Parameter as ChapterSelectParameter );
					break;
				default:
					Logger.Warning( "Loaded Scene Name is Unexpected Name." );
					break;
			}
			
			// staticフィールドなので、一回使ったらパラメータ内を削除
			Parameter = null;

			Logger.Debug( "End" );
		}

	}

}