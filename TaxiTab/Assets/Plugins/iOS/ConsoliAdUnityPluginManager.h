//
//  ConsoliAdUnityPluginManager.h
//  test
//
//  Created by FazalElahi on 09/02/2017.
//  Copyright © 2017 FazalElahi. All rights reserved.
//

@interface ConsoliAdUnityPluginManager : NSObject

+ (void)sendMessageToUnity:(NSString*)gameObjectName method:(NSString*)methodName location:(NSString*)location;

@end
