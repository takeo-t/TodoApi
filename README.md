# 1.プロジェクト名：　TODO API

# 2.アプリケーションの概要
本アプリケーションはTodoアプリケーションのバックエンド部分を担当します。  
主要な機能は下記に示す通りです。また()内に各機能のエンドポイントを示します。  
また、システム構成を下記に示します。  
![システム構成](images/SystemMap.jpeg)  

## 取得(GET)  
##### 全TODOを取得する機能([GET]/api/TodoItems)  
##### 未完了TODOのみ取得する機能([GET]/api/TodoItems?status=0`)  
##### 完了済TODOのみ取得する機能([GET]/api/TodoItems?status=1)  

## 追加(POST)  
##### TODOを追加する機能([POST]/api/TodoItems)  

## 変更(PUT)  
##### TODOを変更する機能([PUT]/api/TodoItems/{id})  
##### 完了(/api/TodoItems/{id}/markIncomplete)  
##### 未完了に戻す(/api/TodoItems/{id}/markComplete)  

## 削除(DELETE)  
##### TODOを削除する機能([DELETE]api/TodoItems/{id})  

# 3.データモデルについて
データモデルを示します。
```Models/TodoItem.cs
namespace TodoApi.Models;

public class TodoItem
{
    public long Id { get; set; }
    public string Title { get; set; } = string.Empty; //string 型で、Todoアイテムのタイトルを表します。
    public string Content { get; set; } = string.Empty; //string 型で、Todoアイテムの内容や詳細を表します。
    public DateTime DateTime { get; set; } //DateTime 型で、Todoアイテムが作成された日時や予定日時など、日時に関連する情報を保持します。
    public TodoStatus Status { get; set; } //TodoStatus 型でTodoアイテムの状態（例：未完了、完了）を示します。
    public DateTime? CompletedAt { get; set; } //DateTime? 型で、Todoアイテムが完了した日時を示します。このプロパティは null 許容型（DateTime?）であるため、値がない場合（つまりアイテムが未完了の場合）は null になります。
}
```

# 4.依存関係について
本プロジェクトの依存関係は`obj/project.assets.json`を参照してください。  

# 5.セットアップ手順
セットアップするにあたり.NETcore7のインストールを前提とします。
```
git clone https://github.com/takeo-t/TodoApi/repository.git //ローカルにセットアップ
```
```
dotnet run --launch-profile https //ビルド
```

#　6.作者情報
Taiki Takeo  
E-mail takeo-t@118satellite.com  