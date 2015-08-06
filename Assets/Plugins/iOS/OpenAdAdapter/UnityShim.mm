
//
//  UnityShim.mm
//  OpenAdAdapter
//
//  Created by Artem Babenko on 7/15/15.
//  Copyright (c) 2015 Artem Babenko. All rights reserved.
//




#include "UnityShim.h"
#import <UIKit/UIKit.h>

#import "OpenAdAdapter.h"


extern UIViewController *UnityGetGLViewController();


void UnityShimOAD_Init(const char * url){
    
    NSString * url1 = [NSString stringWithUTF8String:url];
    
    //[OpenAdAdapter startWithUrl:@"https://raw.githubusercontent.com/sample-data/oad1/master/ios-redir.json"];
    [OpenAdAdapter startWithUrl:url1];
//    UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"OAD"
//                                                    message:url1
//                                                   delegate:nil
//                                          cancelButtonTitle:@"OK"
//                                          otherButtonTitles:nil];
//    [alert show];
}
void UnityShimOAD_ShowTopBanner(){
    [OpenAdAdapter showTopBanner:UnityGetGLViewController()];
    //    UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"OAD"
    //                                                    message:@"UnityShimOAD_ShowBannerTop"
    //                                                   delegate:nil
    //                                          cancelButtonTitle:@"OK"
    //                                          otherButtonTitles:nil];
    //    [alert show];
}


void UnityShimOAD_ShowBottomBanner(){
    [OpenAdAdapter showBottomBanner:UnityGetGLViewController()];
    //    UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"OAD"
    //                                                    message:@"UnityShimOAD_ShowBannerTop"
    //                                                   delegate:nil
    //                                          cancelButtonTitle:@"OK"
    //                                          otherButtonTitles:nil];
    //    [alert show];
}


void UnityShimOAD_HideBanner(){
    [OpenAdAdapter hideBanner];
    //    UIAlertView *alert = [[UIAlertView alloc] initWithTitle:@"OAD"
    //                                                    message:@"UnityShimOAD_ShowBannerTop"
    //                                                   delegate:nil
    //                                          cancelButtonTitle:@"OK"
    //                                          otherButtonTitles:nil];
    //    [alert show];
}
void UnityShimOAD_Fullscreen(){
    [OpenAdAdapter showFullscreen:UnityGetGLViewController()];
}
void UnityShimOAD_Video(){
    [OpenAdAdapter showVideo:UnityGetGLViewController()];
}
void UnityShimOAD_Rewarded(){
    [OpenAdAdapter showRewarded:UnityGetGLViewController()];
}
int UnityShimOAD_GetBannerHeightInPixels(){
    return (int)[OpenAdAdapter bannerHeightPixels];
}
bool UnityShimOAD_HasReward(){
    return [OpenAdAdapter hasReward];
}
const char * UnityShimOAD_CheckReward(){
    OADReward* r1 = [OpenAdAdapter reward];
    if(r1 == nil){
        return nil;
    }
    NSString *s1 = [NSString stringWithFormat:@"amount\t%f\nnetwork\t%@\ncurrency\t%@", [r1 amount], [r1 network], [r1 currency]];
    NSData *bytes = [s1 dataUsingEncoding:NSUTF8StringEncoding];
    const char *rawBytes = (const char *)[bytes bytes];
    const char * s2 = strdup(rawBytes);
    return s2;
}



