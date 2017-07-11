//
//  ConsoliAdIOSPlugin.h
//  test
//
//  Created by FazalElahi on 06/02/2017.
//  Copyright © 2017 FazalElahi. All rights reserved.
//

#import <Foundation/Foundation.h>

@interface ConsoliAdIOSPlugin : NSObject

+ (instancetype)sharedPlugIn;

- (BOOL)initWithKey:(NSString*)appKey andDelegate:(id)adelegate;

- (void)showInterstitial:(NSString*)scene;

- (void)cacheInterstitial:(NSString*)scene;

- (BOOL)hasInterstitial:(NSString*)scene;

- (void)loadInterstitialForScene:(NSString*)scene;

- (BOOL)hasInterstitialForScene:(NSString*)scene;

@end
