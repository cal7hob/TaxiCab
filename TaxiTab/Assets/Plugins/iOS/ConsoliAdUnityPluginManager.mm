//
//  ConsoliAdUnityPluginManager.m
//  test
//
//  Created by FazalElahi on 06/02/2017.
//  Copyright © 2017 FazalElahi. All rights reserved.
//

#import <Foundation/Foundation.h>
#import "ConsoliAdUnityPluginManager.h"
#import "ConsoliAdUnityPlugin.h"

#if UNITY_IOS
void UnitySendMessage(const char *className, const char *methodName, const char *param);
#endif

@implementation ConsoliAdUnityPluginManager

+ (BOOL)initWithKey:(NSString*)appKey andDeviceID:(NSString*)deviceID andGameObject:(NSString*)gameObjectName {
   return [[ConsoliAdUnityPlugin sharedPlugIn] initWithKey:appKey andDeviceID:deviceID andGameObject:gameObjectName];
}

+ (void)showInterstitial:(int)scene {
    [[ConsoliAdUnityPlugin sharedPlugIn] showInterstitial:scene];
}

+ (void)cacheInterstitial:(int)scene {
    [[ConsoliAdUnityPlugin sharedPlugIn] cacheInterstitial:scene];

}

+ (BOOL)hasInterstitial:(int)scene {
    return [[ConsoliAdUnityPlugin sharedPlugIn] hasInterstitial:scene];
}

+ (BOOL)hasInterstitialForScene:(int)scene {
    return [[ConsoliAdUnityPlugin sharedPlugIn] hasInterstitialForScene:scene];
}

+ (void)loadInterstitialForScene:(int)scene {
    [[ConsoliAdUnityPlugin sharedPlugIn] loadInterstitialForScene:scene];
}

+ (void)sendStatsOnPauseWithDeviceID:(NSString*)deviceID {
    [[ConsoliAdUnityPlugin sharedPlugIn] processStatsOnPauseWithDeviceID:deviceID];
}

+ (void)sendMessageToUnity:(NSString*)gameObjectName method:(NSString*)methodName location:(NSString*)location {
#if UNITY_IOS
    UnitySendMessage([gameObjectName UTF8String], [methodName UTF8String], [location UTF8String]);
#endif
}

extern "C" {
    
    bool _initAppWithKey(char *appKey,char *deviceID,char* gameObjectName)
    {
        return [ConsoliAdUnityPluginManager initWithKey:[NSString stringWithUTF8String:appKey] andDeviceID:[NSString stringWithUTF8String:deviceID] andGameObject:[NSString stringWithUTF8String:gameObjectName]];
    }
    
    void _showInterstitial(int scene)
    {
        [ConsoliAdUnityPluginManager showInterstitial:scene];
    }
    
    void _cacheInterstitial(int scene)
    {
        [ConsoliAdUnityPluginManager cacheInterstitial:scene];
    }
    
    void _loadInterstitialForScene(int scene)
    {
        [ConsoliAdUnityPluginManager cacheInterstitial:scene];
    }
    bool _hasInterstitial(int scene)
    {
        return [ConsoliAdUnityPluginManager hasInterstitial:scene];
    }
    bool _hasInterstitialForScene(int scene)
    {
        return [ConsoliAdUnityPluginManager hasInterstitial:scene];
    }
    void _sendStatsOnPauseWithDeviceID(char *deviceID)
    {
        return [ConsoliAdUnityPluginManager sendStatsOnPauseWithDeviceID:[NSString stringWithUTF8String:deviceID]];
    }
}

@end
