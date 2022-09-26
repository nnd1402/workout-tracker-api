﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using WorkoutTracker.API.Data;
using WorkoutTracker.API.Models;

namespace WorkoutTracker.API.Repository
{
    public class WorkoutRepository : IWorkoutRepository
    {
   

        public async Task<IEnumerable<Workout>> GetWorkouts()
        {
            using (var _inMemoryDbContext = new InMemoryDbContext())
            {
                return await _inMemoryDbContext.Workouts.ToListAsync();
            }
        }

        public async Task<Workout> GetWorkout(Guid Id)
        {
            using (var _inMemoryDbContext = new InMemoryDbContext())
            {
                return await _inMemoryDbContext.Workouts
               .FirstOrDefaultAsync(e => e.Id == Id);
            }    
        }

        public async Task<Workout> AddWorkout(Workout workout)
        {
            using (var _inMemoryDbContext = new InMemoryDbContext())
            {
                workout.Date = workout.Date.ToLocalTime();
                var result = await _inMemoryDbContext.Workouts.AddAsync(workout);
                await _inMemoryDbContext.SaveChangesAsync();
                return result.Entity;
            }       
        }

        public async Task<Workout> UpdateWorkout(Workout workout)
        {
            using (var _inMemoryDbContext = new InMemoryDbContext())
            {
                var result = await _inMemoryDbContext.Workouts
               .FirstOrDefaultAsync(e => e.Id == workout.Id);

                if (result != null)
                {
                    result.Title = workout.Title;
                    result.Duration = workout.Duration;
                    result.Description = workout.Description;
                    result.Date = workout.Date;

                    await _inMemoryDbContext.SaveChangesAsync();

                    return result;
                }

                return null;
            }   
        }

        public async Task<Workout> DeleteWorkout(Guid Id)
        {
            using (var _inMemoryDbContext = new InMemoryDbContext())
            {
                var result = await _inMemoryDbContext.Workouts
                             .FirstOrDefaultAsync(e => e.Id == Id);
                if (result != null)
                {
                    _inMemoryDbContext.Workouts.Remove(result);
                    await _inMemoryDbContext.SaveChangesAsync();
                    return result;
                }
                return null;
            }        
        }
    }
}

