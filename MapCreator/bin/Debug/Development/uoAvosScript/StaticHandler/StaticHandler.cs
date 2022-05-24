using System;
using System.Collections;
using System.IO;
using System.Xml;
using Server;
using Server.Gumps;
using Server.Items;
using Server.Scripts.Commands;
using Server.Targeting;

namespace Server.MapCreator
{
	public class Statics
	{
		private static bool EnableLogFile = true; // Creates a log inside your "MapCreator\Export\Logs\" Folder

		private const string ExportPath = @"Q:\MapCreator\Export"; // Set this path to match the MapCreator project folder you are currently working from

		private static string Filename = null;
		private static Map MapID = Map.Internal;

		public static void Initialize()
		{
			Server.Commands.Register( "ExportStatics", AccessLevel.Administrator, new CommandEventHandler(ExportStatics_OnCommand) );
			Server.Commands.Register( "AreaExportStatics", AccessLevel.Administrator, new CommandEventHandler(AreaExportStatics_OnCommand) );	
			Server.Commands.Register( "ImportStatics", AccessLevel.Administrator, new CommandEventHandler(ImportStatics_OnCommand) );
		}

		private static Point3D NullP3D = new Point3D( int.MinValue, int.MinValue, int.MinValue );

		public static void SendWarning( Mobile m, string header, string baseWarning, Map map, Point3D start, Point3D end, WarningGumpCallback callback )
		{
			m.SendGump( new WarningGump( 1060635, 30720, String.Format( baseWarning, String.Format( header, map ) ), 0xFFC000, 420, 400, callback, new StateInfo( map, start, end ) ) );
		}

		private const string BaseFreezeWarning =	"{0}  " +
			"Warning: These items <u>will be removed from the game world</u>, and placed into the <u>MapCreator\Export\Statics\</u> directory.  " +
			"Other players <u>will not see the changes you have made</u> unless you distribute your modified game files to them.<br><br>" +
			"For this process to work correctly, you will need to recompile your map with the exported static files placed inside your <u>CurrentProject/Import/</u> directory.<br>" +
			"For Example: CurrentProject/Import/Building.xml<br>" +
			"Do you wish to proceed?";
		
		public static void ExportStatics_OnCommand( CommandEventArgs e )
		{
			if( e.Length != 0 )
			{
				string iPath = String.Format (@"{0}/Map{1}/Import", ExportPath, e.Mobile.Map.MapIndex);

				if (!Directory.Exists(iPath))
				{
					Directory.CreateDirectory(iPath);
				}
					
				string iFile = String.Format( "{0}/{1}.xml", iPath, e.GetString( 0 ) );
					
				if( File.Exists( iFile ) )
				{
					e.Mobile.SendMessage( "Warning: That filename has already been taken. Choose another." );
					return;
				}

				Filename = iFile;

				Map map = e.Mobile.Map;

				if (map != null && map != Map.Internal)
				{
					SendWarning(e.Mobile, "You are about to freeze <u>all items in {0}</u>.", BaseFreezeWarning, map, NullP3D, NullP3D, new WarningGumpCallback(ExportWarning_Callback));
				}
			}
			else
			{
				e.Mobile.SendMessage("Format: [ExportStatics <Filename>");
			}
		}

		public static void AreaExportStatics_OnCommand( CommandEventArgs e )
		{
			if( e.Length != 0 )
			{
				string iPath = String.Format (@"{0}/Map{1}/Import", ExportPath, e.Mobile.Map.MapIndex);

				if (!Directory.Exists(iPath))
				{
					Directory.CreateDirectory(iPath);
				}
						
				string iFile = String.Format( "{0}/{1}.xml", iPath, e.GetString( 0 ) );
						
				if( File.Exists( iFile ) )
				{
					e.Mobile.SendMessage( "Warning: That filename has already been taken. Choose another." );
					return;
				}

				Filename = iFile;	
					
				BoundingBoxPicker.Begin( e.Mobile, new BoundingBoxCallback( ExportBox_Callback ), null );
			}
			else
			{
				e.Mobile.SendMessage("Format: [AreaExportStatics <Filename>");
			}
		}

		private static void ExportBox_Callback( Mobile from, Map map, Point3D start, Point3D end, object state )
		{
			SendWarning( from, "You are about to freeze a section of items.", BaseFreezeWarning, map, start, end, new WarningGumpCallback( ExportWarning_Callback ) );
		}

		private static void ExportWarning_Callback( Mobile from, bool okay, object state )
		{
			if (!okay)
			{
				return;
			}

			StateInfo si = (StateInfo)state;

			Export( from, si.m_Map, si.m_Start, si.m_End );
		}
		
		public static void Export( Mobile from, Map map, Point3D start, Point3D end )
		{
			try
			{
				if ( start == NullP3D && end == NullP3D )
				{
					XmlTextWriter iXmlFile = new XmlTextWriter( Filename, System.Text.Encoding.UTF8); 

					Console.WriteLine ( "Exporting MC: {0}, {1}", from.Map, Filename );

					iXmlFile.WriteStartDocument();
					iXmlFile.Formatting = Formatting.Indented;
					iXmlFile.Indentation = 4;
					iXmlFile.WriteStartElement("Static_Tiles") ;

					ArrayList remove = new ArrayList();
					int count = 0;

					foreach ( Item item in World.Items.Values )
					{
						if ( ( item is Static || item is BaseFloor || item is BaseWall ) && item.RootParent == null )
						{
							if (item.Map == from.Map)
							{
								iXmlFile.WriteStartElement("Tile");
								iXmlFile.WriteAttributeString ( "TileID", item.ItemID.ToString() );
								iXmlFile.WriteAttributeString ( "X", item.X.ToString() );
								iXmlFile.WriteAttributeString ( "Y", item.Y.ToString() );
								iXmlFile.WriteAttributeString ( "Z", item.Z.ToString());
								iXmlFile.WriteAttributeString ( "Hue", item.Hue.ToString() );
								iXmlFile.WriteEndElement();
						
								count++;
								remove.Add( item );

								if (EnableLogFile)
								{
									CreateLogFile(String.Format("[{5}] Exported: ( TileID: {0}, X: {1}, Y: {2}, Z: {3}, Hue: {4} )", item.ItemID.ToString(), item.X.ToString(), item.Y.ToString(), item.Z.ToString(), item.Hue.ToString(), count), false);
								}
							}
						}
					}

					iXmlFile.WriteEndElement();
					iXmlFile.WriteEndDocument();
					iXmlFile.Close();

					foreach (Item item in remove)
					{
						item.Delete();
					}

					if (count == 0 && File.Exists(Filename))
					{
						File.Delete(Filename);
					}

					from.SendMessage( "Export complete! You exported {0} statics.", count );

					if (EnableLogFile)
					{
						CreateLogFile(String.Format("Export complete! You exported {0} statics.", count), false);
					}
				}
				else
				{
					Rectangle2D rect = new Rectangle2D( start.X, start.Y, end.X - start.X + 1, end.Y - start.Y + 1 );
					
					IPooledEnumerable eable;

					XmlTextWriter iXmlFile = new XmlTextWriter(Filename, System.Text.Encoding.UTF8); 

					Console.WriteLine ( "Exporting MC: {0}, {1}", from.Map, Filename );

					iXmlFile.WriteStartDocument();
					iXmlFile.Formatting = Formatting.Indented;
					iXmlFile.Indentation = 4;
					iXmlFile.WriteStartElement("Static_Tiles") ;

					ArrayList remove = new ArrayList();
					int count = 0;
					
					eable = map.GetObjectsInBounds( rect );
					
					foreach ( object obj in eable )
					{					
						if ( !(obj is Mobile) && ( ((Item)obj) is Static || ((Item)obj) is BaseFloor || ((Item)obj) is BaseWall ) && ((Item)obj).RootParent == null )
						{
							if (((Item)obj).Map == from.Map)
							{
								iXmlFile.WriteStartElement("Tile");
								iXmlFile.WriteAttributeString ( "TileID", ((Item)obj).ItemID.ToString() );
								iXmlFile.WriteAttributeString ( "X", ((Item)obj).X.ToString() );
								iXmlFile.WriteAttributeString ( "Y", ((Item)obj).Y.ToString() );
								iXmlFile.WriteAttributeString ( "Z", ((Item)obj).Z.ToString());
								iXmlFile.WriteAttributeString ( "Hue", ((Item)obj).Hue.ToString() );
								iXmlFile.WriteEndElement();
							
								count++;
								remove.Add( ((Item)obj) );

								if (EnableLogFile)
								{
									CreateLogFile(String.Format("[{5}] Exported: ( TileID: {0}, X: {1}, Y: {2}, Z: {3}, Hue: {4} )", ((Item)obj).ItemID.ToString(), ((Item)obj).X.ToString(), ((Item)obj).Y.ToString(), ((Item)obj).Z.ToString(), ((Item)obj).Hue.ToString(), count), false);
								}
							}
						}
					}

					eable.Free();

					iXmlFile.WriteEndElement();
					iXmlFile.WriteEndDocument();
					iXmlFile.Close();

					foreach (Item item in remove)
					{
						item.Delete();
					}

					if (count == 0 && File.Exists(Filename))
					{
						File.Delete(Filename);
					}

					from.SendMessage( "Exported Complete! You exported {0} statics.", count );

					if (EnableLogFile)
					{
						CreateLogFile(String.Format("Export complete! You exported {0} statics.", count), false);
					}
				}
			}
			catch( Exception ex )
			{
				if (EnableLogFile)
				{
					CreateLogFile(String.Format("Export Crash Generated {0}: \n{1}", DateTime.Now, ex.ToString()), false, true);
				}
			}
		}	
		
		private class StateInfo
		{
			public Map m_Map;
			public Point3D m_Start, m_End;

			public StateInfo( Map map, Point3D start, Point3D end )
			{
				m_Map = map;
				m_Start = start;
				m_End = end;
			}
		}

		private const string BaseUnFreezeWarning =	"{0}  " +
			"This will process the files needed to import your statics, you will have to delete those files manually.<br><br>" +			
			"Do you wish to proceed?";

		public static void ImportStatics_OnCommand( CommandEventArgs e )
		{
			try
			{
				if( e.Length != 0 && e.Length != 1 )
				{
					int mapID = e.GetInt32( 0 );

					switch( mapID )
					{
						case 0:
							{
								MapID = Map.Felucca; 
								break;
							}
						case 1:
							{
								MapID = Map.Trammel; 
								break;
							}
						case 2:
							{
								MapID = Map.Ilshenar; 
								break;
							}
						case 3:
							{
								MapID = Map.Malas; 
								break;
							}
						case 4:
							{
								MapID = Map.Tokuno; 
								break;
							}
						case 5:
							{
								MapID = Map.TerMur; 
								break;
							}
					}			
					
					string iPath = String.Format (@"{0}/Map{1}/Import", ExportPath, mapID );

					if (!Directory.Exists(iPath))
					{
						Directory.CreateDirectory(iPath);
					}

					string iFile = String.Format( "{0}/{1}.xml", iPath, e.GetString( 1 ) );

					if( !File.Exists( iFile ) )
					{
						e.Mobile.SendMessage( "Cannot process file(s): {0}, please retry.", iFile );
						return;
					}

					Filename = iFile;	

					SendWarning( e.Mobile, "You are about to unfreeze from a file.", BaseUnFreezeWarning, null, NullP3D, NullP3D, new WarningGumpCallback( ImportWarning_Callback ) );
				}
				else
				{
					e.Mobile.SendMessage( "Format: [ImportStatics <MapID> <Filename>" );
				}
			}
			catch( Exception ex )
			{
				if( EnableLogFile )
				{
					CreateLogFile( String.Format( "Import Crash Generated {0}: \n{1}", DateTime.Now, ex.ToString() ), false, true );
					
				}
			}
		}

		private static void ImportWarning_Callback( Mobile from, bool okay, object state )
		{
			if (!okay)
			{
				return;
			}

			StateInfo si = (StateInfo)state;

			Import( from );
		}

		public static void Import( Mobile from )
		{
			FileStream fs = new FileStream( Filename, FileMode.Open, FileAccess.Read );
			XmlTextReader xml = new XmlTextReader(fs);

			ReadFileImport(from, xml);
			
			xml.Close();
			fs.Close();
		}

		private static void ReadFileImport( Mobile from, XmlTextReader xml )
		{		
			ArrayList array = new ArrayList();

			int tileid = 0, x = 0, y = 0, z = 0, hue = 0;

			int count = 0;

			while( xml.Read() )
			{
				if (xml.MoveToAttribute("TileID"))
				{
					tileid = Utility.ToInt32(xml.Value);
				}

				if (xml.MoveToAttribute("X"))
				{
					x = Utility.ToInt32(xml.Value);
				}

				if (xml.MoveToAttribute("Y"))
				{
					y = Utility.ToInt32(xml.Value);
				}

				if (xml.MoveToAttribute("Z"))
				{
					z = Utility.ToInt32(xml.Value);
				}

				if (xml.MoveToAttribute("Hue"))
				{
					hue = Utility.ToInt32(xml.Value);
				}

				array.Add( new StaticImportEntry( tileid, x, y, z, hue ) );

				count++;

				if (EnableLogFile)
				{
					CreateLogFile(String.Format("[{5}] ArrayList Property: ( TileID: {0}, X: {1}, Y: {2}, Z: {3}, Hue: {4} )", tileid, x, y, z, hue, count), true);
				}
			}

			count = 0;

			foreach( StaticImportEntry s in array )
			{
				Static item = new Static( s.TileID );

				item.Hue = s.Hue;		
				
				item.X = s.X;
				item.Y = s.Y;
				item.Z = s.Z;

				item.MoveToWorld( new Point3D( s.X, s.Y, s.Z ), MapID );
				item.Movable = false;

				count++;	
			}

			if (EnableLogFile)
			{
				CreateLogFile(String.Format("Imported {0} items to [{1}].", count, MapID.Name), true);
			}

			from.SendMessage( "Imported {0} items to {1}.", count, MapID.Name );
		}

		private static void CreateLogFile( string value, bool import )
		{
			CreateLogFile( value, import, false );
		}

		private static void CreateLogFile( string value, bool import, bool crash )
		{
			if (!Directory.Exists(Core.BaseDirectory + "/Export/Logs")) 
			{
				Directory.CreateDirectory(Core.BaseDirectory + "/Export/Logs");
			}

			if (!Directory.Exists(Core.BaseDirectory + "/Export/Logs/"))
			{
				Directory.CreateDirectory(Core.BaseDirectory + "/Export/Logs/");
			}

			if( !crash )
			{
				FileStream fs = null;

				if (!Directory.Exists(Core.BaseDirectory + "/Export/Logs/Imports") && import)
				{
					Directory.CreateDirectory(Core.BaseDirectory + "/Export/Logs/Imports");
				}

				if (!Directory.Exists(Core.BaseDirectory + "/Export/Logs/Exports") && !import)
				{
					Directory.CreateDirectory(Core.BaseDirectory + "/Export/Logs/Exports");
				}

				if (import)
				{
					fs = new FileStream(Core.BaseDirectory + "/Export/Logs/Imports/ImportLog.log", FileMode.Append);
				}
				else
				{
					fs = new FileStream(Core.BaseDirectory + "/Export/Logs/Exports/ExportLog.log", FileMode.Append);
				}

				StreamWriter writer = new StreamWriter( fs );

				writer.WriteLine( value );

				writer.Close();
				fs.Close();
			}
			else
			{
				if (!Directory.Exists(Core.BaseDirectory + "/Export/Logs/Crash"))
				{
					Directory.CreateDirectory(Core.BaseDirectory + "/Export/Logs/Crash");
				}

				string path = Core.BaseDirectory+ "/Export/Logs/Crash/Log_" + DateTime.Now.ToString()+".log";

				FileStream fs2 = new FileStream( Core.BaseDirectory+ "/Export/Logs/Crash/CrashLog.log", FileMode.Append );
				StreamWriter writer = new StreamWriter( fs2 );

				writer.WriteLine();
				writer.WriteLine();
				writer.WriteLine( "Static Export|Import - Crash Generated on {0}", DateTime.Now );
				writer.WriteLine( "======================================" );
				writer.WriteLine();
				writer.WriteLine();
				writer.WriteLine( "* * *Crash Report* * *" );
				writer.WriteLine( value );

				writer.Close();
				fs2.Close();
			}
		}

		private class StaticImportEntry
		{
			private int m_TileID;

			private int m_X;
			private int m_Y;
			private int m_Z;

			private int m_Hue;

			public int TileID{ get{ return m_TileID; } }

			public int X{ get{ return m_X; } }
			public int Y{ get{ return m_Y; } }
			public int Z{ get{ return m_Z; } }

			public int Hue{ get{ return m_Hue; } }

			public StaticImportEntry( int tileID, int x, int y, int z, int hue )
			{
				m_TileID = tileID;

				m_X = x;
				m_Y = y;
				m_Z = z;

				m_Hue = hue;
			}
		}
	}	
}