<?php

namespace App\Http\Controllers\Admin;

use Illuminate\Http\Request;
use App\Http\Controllers\Controller;
use SebastianBergmann\CodeCoverage\Report\Xml\Project;  
use App\Models\Projects;
use Illuminate\Support\Facades\Storage;

class ProjectController extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        $projects = Projects::orderBy('created_at', 'desc')->get();
        return view('admin.projects', compact('projects'));
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
            'title' => 'required|string|max:255',
            'description' => 'nullable|string',
            'status' => 'required|in:pending,in-progress,completed',
            'model' => 'required|string|max:32',
            'start_date' => 'required|date',
            'end_date' => 'nullable|date',
            'progress' => 'nullable|integer|min:0|max:100',
            'image' => 'nullable|image|mimes:jpeg,png,jpg,gif|max:2048',
            'site_url' => 'nullable|url|max:255',
            'tags' => 'nullable|string'
        ]);

        if ($request->hasFile('image')) {
            $image = $request->file('image');
            $imageName = time() . '.' . $image->getClientOriginalExtension();
            
            // اگر مسیر فایل خارج از پوشه projects باشد، آن را کپی می‌کنیم
            if (!str_contains($image->getPathname(), 'uploads/projects')) {
                $image->move(public_path('uploads/projects'), $imageName);
            } else {
                // اگر عکس در پوشه projects باشد، فقط نام فایل را ذخیره می‌کنیم
                $imageName = basename($image->getPathname());
            }
            
            $data['image'] = 'uploads/projects/' . $imageName;
        }

        // تبدیل تگ‌ها به آرایه و ذخیره در دیتابیس
        if ($request->has('tags')) {
            $data['tags'] = $request->tags;
        }

        Projects::create($data);
        return response()->json(['message' => 'پروژه با موفقیت ثبت شد!']);
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
        $project = Projects::findOrFail($id);
        return response()->json($project);
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, string $id)
    {
        $project = Projects::findOrFail($id);
        
        $data = $request->validate([
            'title' => 'required|string|max:255',
            'description' => 'nullable|string',
            'status' => 'required|in:pending,in-progress,completed',
            'model' => 'required|string|max:32',
            'start_date' => 'required|date',
            'end_date' => 'nullable|date',
            'progress' => 'nullable|integer|min:0|max:100',
            'image' => 'nullable|image|mimes:jpeg,png,jpg,gif|max:2048',
            'site_url' => 'nullable|url|max:255',
            'tags' => 'nullable|string'
        ]);

        if ($request->hasFile('image')) {
            $image = $request->file('image');
            $imageName = time() . '.' . $image->getClientOriginalExtension();
            
            if (!str_contains($image->getPathname(), 'uploads/projects')) {
                $image->move(public_path('uploads/projects'), $imageName);
            } else {
                // اگر عکس در پوشه projects باشد، فقط نام فایل را ذخیره می‌کنیم
                $imageName = basename($image->getPathname());
            }
            
            $data['image'] = 'uploads/projects/' . $imageName;
        }

        // تبدیل تگ‌ها به آرایه و ذخیره در دیتابیس
        if ($request->has('tags')) {
            $data['tags'] = $request->tags;
        }

        $project->update($data);

        return response()->json(['message' => 'پروژه با موفقیت بروزرسانی شد!']);
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(string $id)
    {
        $project = Projects::findOrFail($id);
        $project->delete();
        return response()->json(['message' => 'پروژه با موفقیت حذف شد!']);
    }
}
