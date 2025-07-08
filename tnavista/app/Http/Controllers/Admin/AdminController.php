<?php

namespace App\Http\Controllers\Admin;
use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\Projects;
use App\Models\Article;
use App\Models\Message;
use App\Models\TeamMembers;

class AdminController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        $articlesCount = \App\Models\Article::count();
        $commentsCount = \App\Models\Message::count();
        $newArticlesCount = \App\Models\Article::whereDate('created_at', '>=', now()->subWeek())->count();
        $newCommentsCount = \App\Models\Message::whereDate('created_at', '>=', now()->subWeek())->count();
        $projectsCount = Projects::count();
        $teamCount = \App\Models\TeamMembers::count();
        $newTeamCount = \App\Models\TeamMembers::whereDate('created_at', '>=', now()->subWeek())->count();
        return view('admin.dashboard', compact(
            'teamCount',
            'newTeamCount',
            'projectsCount',
            'newCommentsCount',
            'commentsCount',
            'newArticlesCount',
            'articlesCount'
        ));
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
        //
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
    public function edit(string $id)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, string $id)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(string $id)
    {
        //
    }
}
