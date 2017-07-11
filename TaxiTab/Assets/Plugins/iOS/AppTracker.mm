#import <AppTracker/AppTracker.h>
#import "UnityAppController.h"
#import "UnityView.h"

@interface AppTrackerManager : NSObject <AppModuleDelegate>

@end

@implementation AppTrackerManager

void UnitySendMessage(const char* className, const char* methodName, const char* message);

-(id)init {
    self = [super init];
    
    return self;
}

+(void) setDelegate {
    [AppTracker setAppModuleDelegate:self];
}

+(void) onModuleLoaded:(NSString *)placement {
    //    NSLog(@"AppTracker Unity iOS: Module %@ loaded successfully", placement);
    UnitySendMessage("AppTrackerIOS", "onModuleLoaded",[placement UTF8String]);
}
+(void) onModuleClosed:(NSString *)placement {
    //    NSLog(@"AppTracker Unity iOS: Module %@ closed", placement);
    UnitySendMessage("AppTrackerIOS", "onModuleClosed",[placement UTF8String]);
}
+(void) onModuleClicked:(NSString *)placement {
    //    NSLog(@"AppTracker Unity iOS: Module %@ clicked", placement);
    UnitySendMessage("AppTrackerIOS", "onModuleClicked",[placement UTF8String]);
}
+(void) onModuleCached:(NSString *)placement {
    //    NSLog(@"AppTracker Unity iOS: Module %@ cached successfully", placement);
    UnitySendMessage("AppTrackerIOS", "onModuleCached",[placement UTF8String]);
}
+(void) onModuleFailed:(NSString *)placement error:(NSString *)error cached:(BOOL)iscached {
    //    NSLog(@"AppTracker Unity iOS: Module %@ failed to load - %@", placement, error);
    NSString* msg = [NSString stringWithFormat:@"%@:%@:%@",placement,error,(iscached ?@"yes":@"no")];
    UnitySendMessage("AppTrackerIOS", "onModuleFailed",[msg UTF8String]);
}
+(void) onMediaFinished:(BOOL)viewCompleted {
    NSString * str = (viewCompleted) ? @"yes" : @"no";
    //    NSLog(@"AppTracker Unity iOS: Rewarded Video finished playing - viewCompleted - %@", str);
    UnitySendMessage("AppTrackerIOS", "onMediaFinished",[str UTF8String]);
}

extern "C" {
    UIViewController*	afwGetViewController()	{ return GetAppController().rootViewController; }
    UIView*		afwGetView()		{ return GetAppController().unityView; }
    
    void _startSession (char* apikey, bool enableAutoRecache){
        //[AppTrackerManager initializeEventListeners];
        [AppTrackerManager setDelegate];
        [AppTracker setFramework:@"unity"];
        [AppTracker startSession:[NSString stringWithUTF8String:apikey] withOption:((enableAutoRecache)?AppTrackerEnableAutoCache:AppTrackerDisableAutoCache)];
    }
    
    void _loadModule(char* location)
    {
        [AppTracker loadModule:[NSString stringWithUTF8String:location] viewController:afwGetViewController()];
    }
    
    void _loadModuleWithUserData(char* location, char* userData)
    {
        [AppTracker loadModule:[NSString stringWithUTF8String:location] viewController:afwGetViewController() withUserData:[NSString stringWithUTF8String:userData]];
    }
    
    void _loadModuleToCache(char* location)
    {
        [AppTracker loadModuleToCache:[NSString stringWithUTF8String:location]];
    }
    
    void _loadModuleToCacheWithUserData(char* location, char* userData) {
        [AppTracker loadModuleToCache:[NSString stringWithUTF8String:location] withUserData:[NSString stringWithUTF8String:userData]];
    }
    
    void _destroyModule(char* location)
    {
        [AppTracker destroyModule];
    }
    
    void _closeSession() {
        [AppTracker closeSession];
    }
    
    
    //0 auto 1 landscape 2 portrait
    void _fixAdOrientation(int orientation)
    {
        [AppTracker fixAdOrientation:(AdOrientation)orientation];
    }
    
    void _setCrashHandlerStatus(bool enable)
    {
        [AppTracker setCrashHandlerStatus:enable];
    }
    
    void _setAgeRange(char* range)
    {
        [AppTracker setAgeRange:[NSString stringWithUTF8String:range]];
    }
    void _setGender(char* gender)
    {
        [AppTracker setGender:[NSString stringWithUTF8String:gender]];
    }
    bool _isAdReady(char* location) {
        return [AppTracker isAdReady:[NSString stringWithUTF8String:location]];
    }
}

@end
