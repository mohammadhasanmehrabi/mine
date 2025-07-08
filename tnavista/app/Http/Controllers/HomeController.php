<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use App\Models\Projects;
use App\Models\TeamMembers;
use App\Models\User;

class HomeController extends Controller
{
    public function index()
    {
        // استفاده از cache برای آمار
        $projectsCount = cache()->remember('projects_count', 3600, function () {
            return Projects::count();
        });
        
        $teamCount = cache()->remember('team_count', 3600, function () {
            return TeamMembers::count();
        });
        
        $usersCount = cache()->remember('users_count', 3600, function () {
            return User::count();
        });
        
        return view('home', compact('projectsCount', 'teamCount', 'usersCount'));
    }

    public function projects()
    {
        $projects = Projects::with(['user'])->paginate(12);
        $models = cache()->remember('project_models', 3600, function () {
            return Projects::query()->whereNotNull('model')->pluck('model')->unique()->values();
        });
        return view('projects', compact('projects', 'models'));
    }

    public function team()
    {
        $teamMembers = TeamMembers::paginate(12);
        return view('team', compact('teamMembers'));
    }

    public function articles()
    {
        return view('articles');
    }

    public function contact()
    {
        return view('contact');
    }
}