# MongoDB to Azure Cosmos DB

## Overview
- **Category:** Data Modernization
- **Priority:** P3
- **Languages:** NoSQL/JavaScript/Python
- **Repository Name:** appmodlab-mongodb-to-azure-cosmos-db
- **Organization:** EmeaAppGbb

## Objective
This lab teaches participants how to migrate MongoDB workloads to Azure Cosmos DB using the MongoDB API (vCore or RU-based). It covers schema analysis, index optimization for Cosmos DB, data migration using Azure Database Migration Service or mongodump/mongorestore, and adapting application code for Cosmos DB-specific behaviors (request units, partition keys, consistency levels). This migration provides global distribution, guaranteed SLAs, and eliminates self-managed MongoDB cluster administration.

## Demo Legacy Application
**Business Domain:** Social fitness tracking and community platform for "FitTribe"

The MongoDB database powers a social fitness application where users track workouts, join challenges, follow friends, share achievements, and participate in community-driven fitness programs. It makes heavy use of MongoDB's document model, aggregation pipeline, and geospatial queries.

### Tech Stack
- MongoDB 5.0 Community Edition (self-hosted replica set)
- Node.js 18 with Express.js backend
- Mongoose ODM for schema enforcement
- MongoDB Aggregation Pipeline for analytics
- MongoDB Change Streams for real-time features
- Geospatial indexes for location-based features
- GridFS for workout photo/video storage
- MongoDB Atlas Search (or manual text indexes) for search

### Key Files/Folders Structure
```
fittribe/
├── package.json
├── server.js                          # Express app entry
├── src/
│   ├── models/
│   │   ├── User.js                    # User profile with embedded workouts
│   │   ├── Workout.js                 # Workout log with geospatial data
│   │   ├── Challenge.js               # Community challenges
│   │   ├── Activity.js                # Social activity feed
│   │   └── Achievement.js             # User achievements/badges
│   ├── routes/
│   │   ├── users.js                   # User API
│   │   ├── workouts.js                # Workout logging API
│   │   ├── challenges.js              # Challenge management API
│   │   ├── social.js                  # Social feed, follows, likes
│   │   └── leaderboard.js            # Leaderboard and rankings
│   ├── services/
│   │   ├── workoutAnalytics.js        # Aggregation pipeline for stats
│   │   ├── leaderboardService.js      # Complex aggregation for rankings
│   │   ├── nearbyService.js           # Geospatial queries for nearby users
│   │   └── changeStreamHandler.js     # Real-time notifications
│   └── middleware/
│       └── auth.js                    # JWT authentication
├── migrations/
│   └── seed-data.js                   # Seed data generation script
└── docker-compose.yml                 # MongoDB replica set
```

### MongoDB Collections and Document Structures
```javascript
// users collection
{
  _id: ObjectId, username: String, email: String,
  profile: { height: Number, weight: Number, fitnessLevel: String },
  stats: { totalWorkouts: Number, totalDistance: Number, streakDays: Number },
  following: [ObjectId], followers: [ObjectId],  // Embedded arrays
  achievements: [{ badgeId: String, earnedDate: Date }],
  location: { type: "Point", coordinates: [lng, lat] }  // GeoJSON
}

// workouts collection
{
  _id: ObjectId, userId: ObjectId, type: String,
  duration: Number, distance: Number, calories: Number,
  route: { type: "LineString", coordinates: [[lng, lat], ...] },  // GeoJSON
  heartRate: [{ time: Date, bpm: Number }],  // Large embedded array
  photos: [{ gridfsId: ObjectId, caption: String }],
  date: Date, weather: { temp: Number, condition: String }
}

// challenges collection
{
  _id: ObjectId, title: String, description: String,
  creator: ObjectId, participants: [{ userId: ObjectId, progress: Number }],
  startDate: Date, endDate: Date, targetMetric: String, targetValue: Number,
  leaderboard: [{ userId: ObjectId, score: Number, rank: Number }]
}
```

### Legacy Anti-Patterns / MongoDB-Specific Features
- Unbounded embedded arrays (followers/following can grow to thousands)
- Large documents exceeding 16MB limit (workout heart rate data)
- GridFS for media storage (inefficient for cloud)
- $lookup (join) across collections in aggregation pipelines
- Change Streams requiring replica set (oplog-dependent)
- Geospatial queries with 2dsphere indexes
- Complex multi-stage aggregation pipelines (10+ stages)
- No partition key strategy (random _id distribution)
- Read preference secondary for analytics (replica set-dependent)
- Text indexes for search (limited compared to dedicated search)

## Target Architecture
- **Database:** Azure Cosmos DB for MongoDB (vCore for lift-and-shift or RU-based for cloud-native)
- **Partition Strategy:** userId for workouts, challengeId for challenges (optimized for query patterns)
- **Media Storage:** Azure Blob Storage (replacing GridFS)
- **Search:** Azure AI Search (replacing MongoDB text indexes)
- **Real-Time:** Azure Cosmos DB Change Feed (replacing Change Streams)
- **Analytics:** Azure Synapse Link for Cosmos DB (replacing aggregation pipeline analytics)
- **Caching:** Azure Cache for Redis (for leaderboards and hot data)
- **Application:** Node.js with updated Mongoose connection string
- **Monitoring:** Azure Cosmos DB Insights

### Architecture Description
MongoDB is migrated to Cosmos DB for MongoDB (vCore for maximum compatibility or RU-based for cloud-native optimization). Unbounded arrays are refactored into separate collections with proper partition keys. GridFS media moves to Azure Blob Storage. Complex aggregation pipelines for analytics are offloaded to Azure Synapse Link. Change Streams become Cosmos DB Change Feed. Text search moves to Azure AI Search for better relevance ranking. Leaderboards are cached in Azure Redis. The application requires minimal code changes — Mongoose connects to Cosmos DB with an updated connection string.

## Branch Structure
- `main` — Contains the completed lab with full APPMODLAB.md documentation
- `legacy` — Node.js app with self-hosted MongoDB (Docker replica set)
- `solution` — Node.js app targeting Cosmos DB with schema optimizations
- `step-1-assessment` — Schema analysis, index review, partition key strategy
- `step-2-schema-optimization` — Refactor unbounded arrays, design partition keys
- `step-3-data-migration` — Migrate data using DMS or mongodump/mongorestore
- `step-4-app-adaptation` — Update connection, handle RU-based throttling, add retry
- `step-5-azure-integration` — Blob Storage, AI Search, Synapse Link, Redis

## Lab Content Requirements (APPMODLAB.md)
### Learning Objectives
- Analyze MongoDB schemas for Cosmos DB compatibility and optimization
- Design partition key strategies for Cosmos DB collections
- Migrate data from self-hosted MongoDB to Cosmos DB
- Handle Cosmos DB-specific behaviors (RU throttling, consistency levels)
- Integrate with Azure services for search, analytics, and caching

### Prerequisites
- MongoDB and Node.js experience
- Basic Azure Cosmos DB concepts
- Docker Desktop
- Azure subscription
- Node.js 18+ installed

### Step-by-Step Instructions Outline
1. **Run Legacy App** — Start MongoDB replica set, explore fitness platform features
2. **Analyze Schema** — Review document sizes, index usage, query patterns
3. **Design Partition Keys** — Choose partition keys based on query patterns and data distribution
4. **Refactor Schema** — Address unbounded arrays, move GridFS to Blob Storage
5. **Provision Cosmos DB** — Create Cosmos DB for MongoDB account, configure throughput
6. **Migrate Data** — Use DMS or mongorestore to migrate data
7. **Update Application** — Change connection string, add retry logic, handle throttling
8. **Add Azure Services** — Integrate Blob Storage, AI Search, Redis
9. **Validate** — Test all features, compare performance metrics

### Estimated Duration
4–6 hours

### Key Concepts Covered
- MongoDB to Cosmos DB migration patterns
- Partition key design
- Document schema optimization
- RU-based throughput management
- Cosmos DB Change Feed

## What the Squad Needs to Build
1. **Legacy App Setup:** Node.js Express app with Mongoose, MongoDB replica set via Docker Compose, seed data for 10K+ users, 100K+ workouts, and 50+ challenges. All features (social feed, geospatial, leaderboards, change streams) must work.
2. **Modernization Implementation:** Optimized schema for Cosmos DB, migrated data, updated application with retry logic and RU awareness, Azure Blob Storage for media, AI Search for text queries, Redis for leaderboards. Both vCore and RU-based approaches documented.
3. **Lab Documentation:** APPMODLAB.md with MongoDB-to-Cosmos DB compatibility guide, partition key design patterns, schema refactoring examples, and performance tuning tips.
4. **Infrastructure as Code:** Bicep templates for Azure Cosmos DB, Azure Blob Storage, Azure AI Search, Azure Cache for Redis, and Azure Synapse Link.
5. **CI/CD:** GitHub Actions for infrastructure deployment and application deployment.

## Acceptance Criteria
- [ ] Legacy app runs with MongoDB replica set and all features work
- [ ] Geospatial queries, aggregation pipelines, and change streams function
- [ ] Partition key strategy documented for each collection
- [ ] Schema optimizations address unbounded array issues
- [ ] Data migration completes with no data loss
- [ ] Application works against Cosmos DB with updated connection
- [ ] GridFS media successfully moved to Azure Blob Storage
- [ ] APPMODLAB.md follows the template format with complete frontmatter
- [ ] Step-by-step guide is clear and reproducible
- [ ] All code compiles and runs without errors
