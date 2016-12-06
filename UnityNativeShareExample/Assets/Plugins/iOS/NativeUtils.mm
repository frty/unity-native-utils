//
//  NativeUtils.mm
//


#import <Foundation/Foundation.h>
#import <MessageUI/MessageUI.h>

//Hide error of undef function which is available in unity generated xCode project
//#define FMDEBUG
//#ifdef FMDEBUG
//inline UIViewController* UnityGetGLViewController()
//{
//    return nil;
//}
//#endif

@interface NativeUtils : NSObject<MFMailComposeViewControllerDelegate>

+ (id)sharedInstance;

-(void) sendEmailWithSubject:(NSString*) subject withBody:(NSString*) body withRecepient: (NSString*) recepient withImage: (NSString*) imageDataString;
-(void) shareText: (NSString*) body withURL: (NSString*) urlString withImage:(NSString*) imageDataString withSubject: (NSString*) subject;

@end


@implementation NativeUtils

static NativeUtils * _sharedInstance;

+ (id)sharedInstance {
    
    if (_sharedInstance == nil)  {
        _sharedInstance = [[self alloc] init];
    }
    
    return _sharedInstance;
}


+(NSString*) charToNSString: (char*)text {
    return text ? [[NSString alloc] initWithUTF8String:text] : [[NSString alloc] initWithUTF8String:""];
}

-(void) sendEmailWithSubject:(NSString*) subject withBody:(NSString*) body withRecepient: (NSString*) recepient withImage: (NSString*) imageDataString {
    
    MFMailComposeViewController* composeVC = [[MFMailComposeViewController alloc] init];
    composeVC.mailComposeDelegate = self;
    
    // Configure the fields of the interface.
    if(recepient.length > 0)
        [composeVC setToRecipients:@[recepient]];
    if(subject.length > 0)
        [composeVC setSubject:subject];
    
    if(imageDataString && imageDataString.length > 0) {
        NSData *imageData = [[NSData alloc] initWithBase64EncodedString:imageDataString options:0];
        [composeVC addAttachmentData:imageData mimeType:@"image/png" fileName:@"screeenshoot.png"];
    }
    
    if(body.length > 0)
        [composeVC setMessageBody:body isHTML:NO];
    
    // Present the view controller modally.
    [UnityGetGLViewController() presentViewController:composeVC animated:YES completion:nil];
}

-(void) shareText: (NSString*) body withURL: (NSString*) urlString withImage:(NSString*) imageDataString withSubject: (NSString*) subject {
    
    NSMutableArray *sharingItems = [NSMutableArray new];
    if (body && body.length > 0) {
        [sharingItems addObject:body];
    }
    if (imageDataString && imageDataString.length > 0) {
        NSData *imageData = [[NSData alloc] initWithBase64EncodedString:imageDataString options:0];
        UIImage *image = [[UIImage alloc] initWithData:imageData];
        
        [sharingItems addObject:image];
    }
    if (urlString && urlString.length > 0) {
        [sharingItems addObject:urlString];
    }
    
    UIActivityViewController *activityViewController = [[UIActivityViewController alloc]                                                                initWithActivityItems:sharingItems applicationActivities:nil];
    activityViewController.popoverPresentationController.sourceView = UnityGetGLViewController().view;
    activityViewController.popoverPresentationController.sourceRect = CGRectMake(UnityGetGLViewController().view.frame.size.width/2, UnityGetGLViewController().view.frame.size.height/4, 0, 0);
    
    if(subject && subject.length > 0)
    {
        [activityViewController setValue:subject forKey:@"subject"];
    }
    
    [UnityGetGLViewController() presentViewController:activityViewController animated:YES completion:nil];
    
    
}


- (void)mailComposeController:(MFMailComposeViewController *)controller
          didFinishWithResult:(MFMailComposeResult)result error:(NSError *)error {
    //Can check result here
    // Dismiss the mail compose view controller.
    [UnityGetGLViewController() dismissViewControllerAnimated:YES completion:nil];
}


@end

extern "C"
{
    void showSocialSharing(char* body, char* url, char* imageDataString, char* subject) {
        
        [[NativeUtils sharedInstance] shareText:[NativeUtils charToNSString:body] withURL:[NativeUtils charToNSString:url] withImage:[NativeUtils charToNSString:imageDataString] withSubject:[NativeUtils charToNSString:subject]];
    }
    
    void sendEmail(char* subject, char* body, char* recepient, char* imageDataString) {
        [[NativeUtils sharedInstance] sendEmailWithSubject:[NativeUtils charToNSString:subject] withBody:[NativeUtils charToNSString:body] withRecepient:[NativeUtils charToNSString:recepient] withImage:[NativeUtils charToNSString:imageDataString]];
    }
}


