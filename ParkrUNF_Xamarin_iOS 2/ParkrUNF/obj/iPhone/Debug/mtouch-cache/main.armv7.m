#include "xamarin/xamarin.h"

extern void *mono_aot_module_ParkrUNF_info;
extern void *mono_aot_module_Xamarin_iOS_info;
extern void *mono_aot_module_mscorlib_info;
extern void *mono_aot_module_System_info;
extern void *mono_aot_module_System_Xml_info;
extern void *mono_aot_module_System_Core_info;
extern void *mono_aot_module_Mono_Dynamic_Interpreter_info;
extern void *mono_aot_module_Microsoft_WindowsAzure_Mobile_info;
extern void *mono_aot_module_Newtonsoft_Json_info;
extern void *mono_aot_module_System_Xml_Linq_info;
extern void *mono_aot_module_System_Runtime_Serialization_info;
extern void *mono_aot_module_System_ServiceModel_Internals_info;
extern void *mono_aot_module_Microsoft_CSharp_info;
extern void *mono_aot_module_Mono_CSharp_info;
extern void *mono_aot_module_System_Net_Http_info;
extern void *mono_aot_module_System_Net_Http_Primitives_info;
extern void *mono_aot_module_Microsoft_WindowsAzure_Mobile_Ext_info;
extern void *mono_aot_module_SQLitePCL_Ext_info;
extern void *mono_aot_module_SQLitePCL_info;
extern void *mono_aot_module_Microsoft_WindowsAzure_Mobile_SQLiteStore_info;

void xamarin_register_modules ()
{
	mono_aot_register_module (mono_aot_module_ParkrUNF_info);
	mono_aot_register_module (mono_aot_module_Xamarin_iOS_info);
	mono_aot_register_module (mono_aot_module_mscorlib_info);
	mono_aot_register_module (mono_aot_module_System_info);
	mono_aot_register_module (mono_aot_module_System_Xml_info);
	mono_aot_register_module (mono_aot_module_System_Core_info);
	mono_aot_register_module (mono_aot_module_Mono_Dynamic_Interpreter_info);
	mono_aot_register_module (mono_aot_module_Microsoft_WindowsAzure_Mobile_info);
	mono_aot_register_module (mono_aot_module_Newtonsoft_Json_info);
	mono_aot_register_module (mono_aot_module_System_Xml_Linq_info);
	mono_aot_register_module (mono_aot_module_System_Runtime_Serialization_info);
	mono_aot_register_module (mono_aot_module_System_ServiceModel_Internals_info);
	mono_aot_register_module (mono_aot_module_Microsoft_CSharp_info);
	mono_aot_register_module (mono_aot_module_Mono_CSharp_info);
	mono_aot_register_module (mono_aot_module_System_Net_Http_info);
	mono_aot_register_module (mono_aot_module_System_Net_Http_Primitives_info);
	mono_aot_register_module (mono_aot_module_Microsoft_WindowsAzure_Mobile_Ext_info);
	mono_aot_register_module (mono_aot_module_SQLitePCL_Ext_info);
	mono_aot_register_module (mono_aot_module_SQLitePCL_info);
	mono_aot_register_module (mono_aot_module_Microsoft_WindowsAzure_Mobile_SQLiteStore_info);

}

void xamarin_register_assemblies ()
{
	xamarin_open_and_register ("Xamarin.iOS.dll");
	xamarin_open_and_register ("Microsoft.WindowsAzure.Mobile.Ext.dll");

}

void xamarin_create_classes();
void xamarin_setup ()
{
	xamarin_use_old_dynamic_registrar = FALSE;
	xamarin_create_classes();
	xamarin_enable_debug_tracking = FALSE;
	xamarin_init_mono_debug = TRUE;
	xamarin_executable_name = "ParkrUNF.exe";
	xamarin_use_new_assemblies = 1;
	mono_use_llvm = FALSE;
	xamarin_log_level = 0;
	xamarin_use_sgen = TRUE;
	xamarin_debug_mode = TRUE;
	xamarin_new_refcount = TRUE;
	setenv ("MONO_GC_PARAMS", "nursery-size=512k", 1);
}

int main (int argc, char **argv)
{
	NSAutoreleasePool *pool = [[NSAutoreleasePool alloc] init];
	int rv = xamarin_main (argc, argv, false);
	[pool drain];
	return rv;
}
