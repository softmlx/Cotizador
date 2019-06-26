function scroll_to_class(element_class, removed_height) 
{
    var scroll_to = $(element_class).offset().top - removed_height;
    if($(window).scrollTop() != scroll_to) 
    {
        $('html, body').stop().animate({scrollTop: scroll_to}, 0);
    }
}
function bar_progress(progress_line_object, direction) 
{
    var number_of_steps = progress_line_object.data('number-of-steps');
    var now_value = progress_line_object.data('now-value');
    var new_value = 0;
    if(direction == 'right') 
    {
        new_value = now_value + ( 100 / number_of_steps );
    }
    else if(direction == 'left') {
        new_value = now_value - ( 100 / number_of_steps );
    }
    progress_line_object.attr('style', 'width: ' + new_value + '%;').data('now-value', new_value);
}
jQuery(document).ready(function() 
{

    $('form fieldset:first').fadeIn('slow');
    
    $('form input[type="text"], form input[type="password"]').on('focus', function() 
    {
    	$(this).removeClass('input-error');
    });
    
    $('form .btn-next').on('click', function() 
    {
    	var parent_fieldset = $(this).parents('fieldset');
    	var next_step = true;
    	var current_active_step = $(this).parents('form').find('.form-wizard.active');
    	var progress_line = $(this).parents('form').find('.progress-line');

    	/*parent_fieldset.find('input[type="text"], input[type="number"], input[type="password"], input[type="email"]').each(function() 
        {
    		if( $(this).val() == "" ) 
            {
    			$(this).addClass('input-error');
    			next_step = false;
    		}
    		else {
    			$(this).removeClass('input-error');
    		}
    	});*/


        parent_fieldset.find('input[type="text"], input[type="number"], input[type="password"], input[type="email"]').each(function() 
        {
            if ($("#formularioC").is(':hidden'))
            {
                next_step = true;
            } 
            else 
            {
                if( $(this).val() == "" ) 
                {
                    $(this).addClass('input-error');
                    next_step = false;
                }
                else 
                {
                    $(this).removeClass('input-error');
                }
            }
        }); 	
    	
    	if(next_step) 
        {
    		parent_fieldset.fadeOut(400, function() 
            {
    			current_active_step.removeClass('active').addClass('activated').next().addClass('active');
    			bar_progress(progress_line, 'right');
	    		$(this).next().fadeIn();
    			scroll_to_class( $('form'), 20 );
	    	});
    	}
    	
    });
    
    // previous step
    $('form .btn-previous').on('click', function() 
    {
    	var current_active_step = $(this).parents('form').find('.form-wizard.active');
    	var progress_line = $(this).parents('form').find('.progress-line');
    	
    	$(this).parents('fieldset').fadeOut(400, function() 
        {
    		current_active_step.removeClass('active').prev().removeClass('activated').addClass('active');
    		bar_progress(progress_line, 'left');
    		$(this).prev().fadeIn();
			scroll_to_class( $('form'), 20 );
    	});
    });
    
    /*('form').on('submit', function(e) 
    {
    	$(this).find('input[type="text"], input[type="number"], input[type="password"], input[type="email"],textarea').each(function() 
        {
    		if( $(this).val() == "" ) 
            {
    			e.preventDefault();
    			$(this).addClass('input-error');
    		}
    		else {
    			$(this).removeClass('input-error');
    		}
    	});
    });
    */
});