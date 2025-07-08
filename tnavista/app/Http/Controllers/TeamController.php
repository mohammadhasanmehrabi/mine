<?php

namespace App\Http\Controllers;

use App\Models\TeamMembers;
use Illuminate\Http\Request;

class TeamController extends Controller
{
    public function index()
    {
        $teamMembers = TeamMembers::all();
        return view('team', compact('teamMembers'));
    }
} 