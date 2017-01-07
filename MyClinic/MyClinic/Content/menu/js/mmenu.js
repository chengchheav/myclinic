$(function() {
				$("#menu")
					.mmenu({
						extensions: ["iconbar"]
					}).on( 'click',
						'a[href^="#/"]',
						function() {
							alert( "Thank you for clicking, but that's a demo link." );
							return false;
						}
					);
			});