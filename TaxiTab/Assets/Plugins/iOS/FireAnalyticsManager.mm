
//
//  FireAnalytics.c
//  FireAnalytics
//
//  Created by MohsinMahmood on 31/10/2016.
//  Copyright © 2016 ConsoliAds. All rights reserved.
//
#import "FireAnalytics.h"

@interface FireAnalyticsManager : NSObject

@end

@implementation FireAnalyticsManager


+(void)initializeWithLog:(BOOL)logEnabled
{
    [FireAnalytics initializeWithLog:logEnabled];
}

+ (void)logEventName:(nonnull NSString * )eventName action:(nonnull NSString *)action sceneType:(nonnull NSString *)sceneType
{
    [FireAnalytics logEventName:eventName action:action sceneType:sceneType];
}

+(void)logSelectContentWithItemID:(nonnull NSString *)itemID contentType:(nonnull NSString *)action
{
    [FireAnalytics logSelectContentWithItemID:itemID contentType:action];
}

+(void)logJoinGroupID:(nonnull NSString *)groupID
{
    [FireAnalytics logJoinGroupID:groupID];
}

+(void)logLevelUPWithCharacter:(nonnull NSString *)character level:(long)level
{
    [FireAnalytics logLevelUPWithCharacter:character level:level];
}

+(void)logPostScore:(long)Score level:(long)level character:(nonnull NSString *)character
{
    [FireAnalytics logPostScore:Score level:level character:character];
}

+(void)logPostScore:(long)Score level:(long)level
{
    [FireAnalytics logPostScore:Score level:level];
}

+(void)logPostScore:(long)Score
{
    [FireAnalytics logPostScore:Score];
}

+(void)logSpendVirtualCurrencyName:(nonnull NSString *)virtualCurrency itemName:(nonnull NSString *)itemName value:(long)value
{
    [FireAnalytics logSpendVirtualCurrencyName:virtualCurrency itemName:itemName value:value];
}

+(void)logTutorialBegin
{
    [FireAnalytics logTutorialBegin];
}

+(void)logTutorialComplete
{
    [FireAnalytics logTutorialComplete];
}

+(void)logUnlockAchievementWithID:(nonnull NSString *)achievementID
{
    [FireAnalytics logUnlockAchievementWithID:achievementID];
}

extern "C" {
    void _initializeWithLog(bool logEnabled)
    {
        [FireAnalyticsManager initializeWithLog:logEnabled];
    }
    
    void _logEventName(char* eventName, char* action, char* sceneType)
    {
        [FireAnalyticsManager logEventName:[NSString stringWithUTF8String:eventName] action:[NSString stringWithUTF8String:action] sceneType:[NSString stringWithUTF8String:sceneType]];
    }
    
    void _logSelectContentWithItemID(char* itemID, char* contentType)
    {
        [FireAnalyticsManager logSelectContentWithItemID:[NSString stringWithUTF8String:itemID] contentType:[NSString stringWithUTF8String:contentType]];
    }
    
    void _logJoinGroupID(char* groupID)
    {
        [FireAnalyticsManager logJoinGroupID:[NSString stringWithUTF8String:groupID]];
    }
    
    void _logLevelUPWithCharacter(char* character,long level)
    {
        [FireAnalyticsManager logLevelUPWithCharacter:[NSString stringWithUTF8String:character] level:level];
    }
    
    void _logPostScoreWithCharacter(long Score,long level,char* character)
    {
        [FireAnalyticsManager logPostScore:Score level:level character:[NSString stringWithUTF8String:character]];
    }
    
    void _logPostScoreWithLevel(long Score,long level)
    {
        [FireAnalyticsManager logPostScore:Score level:level];
    }
    
    void _logPostScore(long Score)
    {
        [FireAnalyticsManager logPostScore:Score];
    }
    
    void _logSpendVirtualCurrencyName(char* virtualCurrency, char* itemName, long value)
    {
        [FireAnalyticsManager logSpendVirtualCurrencyName:[NSString stringWithUTF8String:  virtualCurrency] itemName:[NSString stringWithUTF8String:itemName] value:value];
    }
    
    void _logTutorialBegin()
    {
        [FireAnalyticsManager logTutorialBegin];
    }
    
    void _logTutorialComplete()
    {
        [FireAnalyticsManager logTutorialComplete];
    }
    
    void _logUnlockAchievementWithID(char * achievementID)
    {
        [FireAnalyticsManager logUnlockAchievementWithID:[NSString stringWithUTF8String:achievementID]];
    }
    
}
@end
    
