//
//  FireAnalytics.h
//  FireAnalytics
//
//  Created by MohsinMahmood on 10/10/2016.
//  Copyright © 2016 ConsoliAds. All rights reserved.
//

#import <Foundation/Foundation.h>
@interface FireAnalytics : NSObject
-(nonnull instancetype) init __attribute__((unavailable("init not available call instance class method to get Object")));

+(void)initializeWithLog:(BOOL)logEnabled;
+(void)logEventName:(nonnull NSString*)eventName action:(nonnull NSString*)action sceneType:(nonnull NSString*)sceneType;
+(void)logSelectContentWithItemID:(nonnull NSString*)itemID contentType:(nonnull NSString*)action;
+(void)logJoinGroupID:(nonnull NSString*)groupID;
+(void)logLevelUPWithCharacter:(nonnull NSString*)character level:(long)level;
+(void)logPostScore:(long)Score;
+(void)logPostScore:(long)Score level:(long)level;
+(void)logPostScore:(long)Score level:(long)level character:(nonnull NSString*)character;
+(void)logSpendVirtualCurrencyName:(nonnull NSString*)virtualCurrency itemName:(nonnull NSString*)itemName value:(long)value;
+(void)logTutorialBegin;
+(void)logTutorialComplete;
+(void)logUnlockAchievementWithID:(nonnull NSString*)achievementID;
//Use for sending custom  Stats
+(void)logEventName:(nonnull NSString*)eventName parametersDict:(nonnull NSDictionary*)param;


@end
