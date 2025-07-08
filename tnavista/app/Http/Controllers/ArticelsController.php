<?php

namespace App\Http\Controllers;

use App\Models\Article;
use Illuminate\Http\Request;

class ArticelsController extends Controller
{
    /**
     * نمایش لیست مقالات
     */
    public function index()
    {
        $articles = Article::orderBy('published_at', 'desc')->paginate(12);
        return view('articles', compact('articles'));
    }

    /**
     * نمایش مقاله خاص
     */
    public function show($id)
    {
        $article = Article::findOrFail($id);
        return view('article-detail', compact('article'));
    }
}
