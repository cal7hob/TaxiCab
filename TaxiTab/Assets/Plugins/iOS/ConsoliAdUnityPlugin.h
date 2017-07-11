//
//  ConsoliAdUnityPlugin.h
//  test
//
//  Created by FazalElahi on 06/02/2017.
//  Copyright © 2017 FazalElahi. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface ConsoliAdUnityPlugin : NSObject

+ (ConsoliAdUnityPlugin*)sharedPlugIn;

- (BOOL)initWithKey:(NSString*)appKey andDeviceID:(NSString*)deviceID andGameObject:(NSString*)gameObjectName_;

- (void)showInterstitial:(int)scene;

- (void)cacheInterstitial:(int)scene;

- (BOOL)hasInterstitial:(int)scene;

- (void)loadInterstitialForScene:(int)scene;

- (BOOL)hasInterstitialForScene:(int)scene;

- (void)processStatsOnPauseWithDeviceID:(NSString*)deviceID;

@end
