

//
//  UnityShim.h
//  OpenAdAdapter
//
//  Created by Artem Babenko on 7/15/15.
//  Copyright (c) 2015 Artem Babenko. All rights reserved.
//






extern "C" void UnityShimOAD_Init(const char * url);
extern "C" void UnityShimOAD_ShowTopBanner();
extern "C" void UnityShimOAD_ShowBottomBanner();
extern "C" void UnityShimOAD_HideBanner();
extern "C" void UnityShimOAD_Fullscreen();
extern "C" void UnityShimOAD_Video();
extern "C" void UnityShimOAD_Rewarded();
extern "C" int UnityShimOAD_GetBannerHeightInPixels();
extern "C" bool UnityShimOAD_HasReward();
extern "C" const char * UnityShimOAD_CheckReward();







