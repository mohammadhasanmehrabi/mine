<?php

namespace App\Http\Controllers\Admin;

use App\Http\Controllers\Controller;
use App\Models\Article;
use Illuminate\Http\Request;
use Carbon\Carbon;

class ArticleController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        $articles = Article::orderBy('published_at', 'desc')->get();
        return view('admin.article', compact('articles'));
    }

    /**
     * Show the form for creating a new resource.
     */
    public function create()
    {
        //
    }

    /**
     * Store a newly created resource in storage.
     */
    public function store(Request $request)
    {
        $data = $request->validate([
            'title' => 'required|string|max:200',
            'summary' => 'nullable|string',
            'content' => 'required|string',
            'author_name' => 'nullable|string|max:100',
            'published_at' => 'required|date'
        ]);

        Article::create($data);
        return response()->json(['message' => 'مقاله با موفقیت ثبت شد!']);
    }

    /**
     * Display the specified resource.
     */
    public function show(string $id)
    {
        //
    }

    /**
     * Show the form for editing the specified resource.
     */
    public function edit($id)
    {
        $article = Article::findOrFail($id);
        return response()->json($article);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, $id)
    {
        $article = Article::findOrFail($id);
        
        $data = $request->validate([
            'title' => 'required|string|max:200',
            'summary' => 'nullable|string',
            'content' => 'required|string',
            'author_name' => 'nullable|string|max:100',
            'published_at' => 'required|date'
        ]);

        $article->update($data);
        return response()->json(['message' => 'مقاله با موفقیت بروزرسانی شد!']);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy($id)
    {
        $article = Article::findOrFail($id);
        $article->delete();
        return response()->json(['message' => 'مقاله با موفقیت حذف شد!']);
    }
}
