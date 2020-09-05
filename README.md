# SwimTrainingWebApplication

## What I'm building

An app to track both in pool and land training sessions for swimmers as well as competitions attended. I'll be attempting to build API back-end application, a common library for shared data transfer objects using .NET Standard and a web front-end application.

## Purpose

The purpose of this project is to allow me to learn GitHub and ASP.NET while building an application for a subject that I'm passionate about. 

## Application Architecture

## Data Schema

## Data Dictionary

**Table name - Coaches**
|Field Name|Data Type|Field Size For Display|Example|
|----------|---------|----------------------|-------|
|Id|interger|5|00001|
|FirstName|varchar|50|John|
|LastName|varchar|50|Smith|
|UserName|varchar|200|John Smith|
|CoachesDetails|varchar|2000|Level 2 Swim Coach|

**Table name - Swimmers**
|Field Name|Data Type|Field Size For Display|Example|
|----------|---------|----------------------|-------|
|Id|interger|5|00001|
|FirstName|varchar|50|John|
|LastName|varchar|50|Smith|
|UserName|varchar|100|John Smith|
|EmailAddress|varchar|100|john.smith@email.com|

**Table name - Squad**
|Field Name|Data Type|Field Size For Display|Example|
|----------|---------|----------------------|-------|
|Id|interger|5|00001|
|Name|varchar|50|Development|
|EventId|integer|5|00001|

**Table name - Event**
|Field Name|Data Type|Field Size For Display|Example|
|----------|---------|----------------------|-------|
|Id|interger|5|00001|
|Name|varchar|50|Training|

**Table name - Sessions**
|Field Name|Data Type|Field Size For Display|Example|
|----------|---------|----------------------|-------|
|Id|interger|5|00001|
|EventId|integer|5|00001|
|SquadId|interger|5|00001|
|Title|varchar|200|Training|
|Description|varchar|4000|Land training at the leisure centre|
|StartTime|date|14|dd/mm/yy HH24:MI|
|EndTime|date|14|dd/mm/yy HH24:MI|

**Table name - EventSwimmer**
|Field Name|Data Type|Field Size For Display|Example|
|----------|---------|----------------------|-------|
|EventId|interger|5|00001|
|SwimmerId|interger|5|00001|

**Table name - SessionSwimmer**
|Field Name|Data Type|Field Size For Display|Example|
|----------|---------|----------------------|-------|
|SessionId|interger|5|00001|
|SwimmerId|interger|5|00001|

**Table name - SessionSwimmer**
|Field Name|Data Type|Field Size For Display|Example|
|----------|---------|----------------------|-------|
|SessionId|interger|5|00001|
|SwimmerId|interger|5|00001|

**Table name - SessionCoach**
|Field Name|Data Type|Field Size For Display|Example|
|----------|---------|----------------------|-------|
|SessionId|interger|5|00001|
|CoachId|interger|5|00001|

**Table name - Tags**
|Field Name|Data Type|Field Size For Display|Example|
|----------|---------|----------------------|-------|
|Id|interger|5|00001|
|Name|varchar|50|Strength & Conditioning|

**Table name - SessionsTag**
|Field Name|Data Type|Field Size For Display|Example|
|----------|---------|----------------------|-------|
|TagsId|interger|5|00001|
|SessionsId|interger|5|00001|
